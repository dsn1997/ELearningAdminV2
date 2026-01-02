using AutoMapper;
using IIG.Application.BackgroundJob.Dtos;
using IIG.Application.Data;
using IIG.Application.Models.Keycodes;
using IIG.Application.Services;
using IIG.Application.Services.Redis;
using IIG.Core.Common.Enums;
using IIG.Core.Common.MongoDataModels.Keycodes;
using IIG.Core.Interface;
using IIG.Core.Providers.BackgroudJob;
using IIG.Core.Providers.Interfaces;
using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using IIG.Core.Providers.RabbitMQProvider;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace IIG.Web.BL.Services.Impls
{
    public class RabbitMockTestWrapperKeyCodeListener : IRabbitListener
    {
        private readonly IRabbitMQFactory _rabbitMQFactory;
        private IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<RabbitMockTestWrapperKeyCodeListener> _logger;
        public RabbitMockTestWrapperKeyCodeListener(IRabbitMQFactory rabbitMQFactory, IServiceProvider serviceProvider,
            ILogger<RabbitMockTestWrapperKeyCodeListener> logger)
        {
            _rabbitMQFactory = rabbitMQFactory;
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        private void BuildExchangeDeclare()
        {

            #region MockTest redis to mongo
            if(true)
            {
                string exchangeName = $"{Core.Common.ConfigureModels.Constants.RabbitMQ.MockTestWrapperKeyCode.ExchangeMockTestRedisToMongo}";
                string queueName = $"{Core.Common.ConfigureModels.Constants.RabbitMQ.MockTestWrapperKeyCode.QueueMockTestRedisToMongo}";
                string routineName = $"{Core.Common.ConfigureModels.Constants.RabbitMQ.MockTestWrapperKeyCode.QueueMockTestRedisToMongoRoutingkey}";

                _channel.ExchangeDeclare(exchange: exchangeName, type: "topic");
                _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false);
                _channel.QueueBind(queue: queueName,
                  exchange: exchangeName
                  , routingKey: routineName);
            }
            #endregion

        }

        public Task RegisterAsync(CancellationToken token)
        {
            _channel = _rabbitMQFactory.Connection.CreateModel();
            BuildExchangeDeclare();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = JsonConvert.DeserializeObject<RabbitMQMockTestWrapperKeyCodeActionModel>(Encoding.UTF8.GetString(body));
                _logger.LogInformation($"Message from redis to mongo: {message}");
                using (var scope = _serviceProvider.CreateScope())
                {
                    var redisFactory = scope.ServiceProvider.GetRequiredService<IRedisGenericFactory>();
                    var _unitOfWorkManager = scope.ServiceProvider.GetRequiredService<IUnitOfWorkManager>();
                    await using (var uow = _unitOfWorkManager.Begin())
                    {
                        switch (message.Action)
                        {
                            case RabbitMQMockTestWrapperKeyCodeAction.StartDoingTest:
                                {

                                    var keyCode = message.KeyCode;

                                    _logger.LogInformation($"MockTestWrapperKeyCode_StartDoingTest ---------- keycode: {keyCode} --- Start");

                                    var _mongoMockTestKeyCodeService = scope.ServiceProvider.GetRequiredService<IMongoGenericRepository<MgMockTestKeyCodeModel>>();
                                    var _mockTestKeyCodeDataService = scope.ServiceProvider.GetRequiredService<IMockTestKeyCodeRedisDataService>();
                                    var _mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
                                    var _mockTestDA = scope.ServiceProvider.GetRequiredService<IMockTestDA>();

                                    //get data from redis
                                    var redisMockTestKeyCodeData = await _mockTestKeyCodeDataService.GetMockTestKeyCodeAsync(keyCode);

                                    //add data to mongo
                                    var mongoMockTestKeyCodeData = await _mongoMockTestKeyCodeService.FindByIdAsync(p => p.KeyCode == keyCode);
                                    if (mongoMockTestKeyCodeData == null)
                                    {
                                        await _mongoMockTestKeyCodeService.InsertOneAsync(redisMockTestKeyCodeData);
                                    }
                                    else
                                    {
                                        redisMockTestKeyCodeData.Id = mongoMockTestKeyCodeData.Id;
                                        await _mongoMockTestKeyCodeService.ReplaceOneAsync(redisMockTestKeyCodeData);
                                    }

                                    //update started doing answer in sql
                                    var startedDoingModelAdd = _mapper.Map<StartedDoingAnswerModel>(redisMockTestKeyCodeData);
                                    await _mockTestDA.StartedDoingAnswerAsync(startedDoingModelAdd);

                                    //await _mockTestKeyCodeDataService.ClearCountMockTestKeyCodeExamining();

                                    _logger.LogInformation($"MockTestWrapperKeyCode_StartDoingTest ---------- keycode: {keyCode} --- End");

                                    break;
                                }
                            case RabbitMQMockTestWrapperKeyCodeAction.SaveAnswer:
                                {

                                    var _mongoMockTestKeyCodeAnswer = scope.ServiceProvider.GetRequiredService<IMongoGenericRepository<MgKeyCodeAnswerModel>>();
                                    var _mongoMockTestKeyCodeService = scope.ServiceProvider.GetRequiredService<IMongoGenericRepository<MgMockTestKeyCodeModel>>();
                                    var _mockTestKeyCodeDataService = scope.ServiceProvider.GetRequiredService<IMockTestKeyCodeRedisDataService>();

                                    var request = JsonConvert.DeserializeObject<SaveAnswerRequest>(message.Data);
                                    _logger.LogInformation($"MockTestWrapperKeyCode_SaveAnswer ---------- keycode: {request.KeyCode} --- questionId: {request.QuestionId} ----- Start ");

                                    //Insert keyCode answer

                                    switch (request.QuestionnaireType)
                                    {
                                        case EQuestionnaireType.Video:
                                        case EQuestionnaireType.Slide:
                                        case EQuestionnaireType.MCQ:
                                        case EQuestionnaireType.PronunciationRecognition:
                                        case EQuestionnaireType.FlashCard:
                                        case EQuestionnaireType.MCQImage:
                                        case EQuestionnaireType.TrueFalse:
                                        case EQuestionnaireType.Record:
                                        case EQuestionnaireType.ReadTextALoud:
                                        case EQuestionnaireType.Writing:
                                            {

                                                var listAnswers = await _mockTestKeyCodeDataService.GetKeyCodeAnswerAsync(request.KeyCode);
                                                var redisMockTestKeyCodeAnswerData = listAnswers.FirstOrDefault(x => x.KeyCode == request.KeyCode &&
                                                                            x.MockTestSectionId ==
                                                                            request.MockTestSectionId &&
                                                                            x.MockTestPartId == request.MockTestPartId &&
                                                                            x.QuestionnaireId == request.QuestionnaireId &&
                                                                            x.QuestionId == request.QuestionId);
                                                await _mongoMockTestKeyCodeAnswer.DeleteManyAsync(x => x.KeyCode == request.KeyCode &&
                                                                                                      x.MockTestSectionId ==
                                                                                                      request.MockTestSectionId &&
                                                                                                      x.MockTestPartId == request.MockTestPartId &&
                                                                                                      x.QuestionnaireId == request.QuestionnaireId &&
                                                                                                      x.QuestionId == request.QuestionId);



                                                if (redisMockTestKeyCodeAnswerData != null)
                                                    await _mongoMockTestKeyCodeAnswer.InsertOneAsync(redisMockTestKeyCodeAnswerData);

                                                break;
                                            }

                                        case EQuestionnaireType.Droplist:
                                            {
                                                var listAnswers = await _mockTestKeyCodeDataService.GetKeyCodeAnswerAsync(request.KeyCode);
                                                var redisMockTestKeyCodeAnswerData = listAnswers.FirstOrDefault(x => x.KeyCode == request.KeyCode &&
                                                                                                    x.MockTestSectionId ==
                                                                                                    request.MockTestSectionId &&
                                                                                                    x.MockTestPartId == request.MockTestPartId &&
                                                                                                    x.QuestionnaireId == request.QuestionnaireId &&
                                                                                                    x.QuestionId == request.QuestionId &&
                                                                                                    x.AnswerId == request.AnswerId);

                                                await _mongoMockTestKeyCodeAnswer.DeleteManyAsync(x => x.KeyCode == request.KeyCode &&
                                                                                                      x.MockTestSectionId ==
                                                                                                      request.MockTestSectionId &&
                                                                                                      x.MockTestPartId == request.MockTestPartId &&
                                                                                                      x.QuestionnaireId == request.QuestionnaireId &&
                                                                                                      x.QuestionId == request.QuestionId &&
                                                                                                      x.AnswerId == request.AnswerId);

                                                if (redisMockTestKeyCodeAnswerData != null)
                                                    await _mongoMockTestKeyCodeAnswer.InsertOneAsync(redisMockTestKeyCodeAnswerData);
                                                break;
                                            }

                                        case EQuestionnaireType.ImageDragDrop:
                                        case EQuestionnaireType.FillInTheBlank:
                                            {


                                                await _mongoMockTestKeyCodeAnswer.DeleteManyAsync(x => x.KeyCode == request.KeyCode &&
                                                                                                      x.MockTestSectionId ==
                                                                                                      request.MockTestSectionId &&
                                                                                                      x.MockTestPartId == request.MockTestPartId &&
                                                                                                      x.QuestionnaireId ==
                                                                                                      request.QuestionnaireId &&
                                                                                                      x.QuestionId == request.QuestionId &&
                                                                                                      x.AnswerId == request.AnswerId);

                                                var listAnswers = await _mockTestKeyCodeDataService.GetKeyCodeAnswerAsync(request.KeyCode);
                                                var redisMockTestKeyCodeAnswerData = listAnswers.FirstOrDefault(x => x.KeyCode == request.KeyCode &&
                                                                                                      x.MockTestSectionId ==
                                                                                                      request.MockTestSectionId &&
                                                                                                      x.MockTestPartId == request.MockTestPartId &&
                                                                                                      x.QuestionnaireId ==
                                                                                                      request.QuestionnaireId &&
                                                                                                      x.QuestionId == request.QuestionId &&
                                                                                                      x.AnswerId == request.AnswerId);

                                                if (!string.IsNullOrEmpty(request.AnswerText) && redisMockTestKeyCodeAnswerData != null)
                                                {
                                                    await _mongoMockTestKeyCodeAnswer.InsertOneAsync(redisMockTestKeyCodeAnswerData);
                                                }
                                                break;
                                            }
                                        case EQuestionnaireType.Matching:
                                        case EQuestionnaireType.MatchingImage:
                                            {


                                                await _mongoMockTestKeyCodeAnswer.DeleteManyAsync(x => x.KeyCode == request.KeyCode &&
                                                                                                      x.MockTestSectionId ==
                                                                                                      request.MockTestSectionId &&
                                                                                                      x.MockTestPartId == request.MockTestPartId &&
                                                                                                      x.QuestionnaireId ==
                                                                                                      request.QuestionnaireId &&
                                                                                                      x.QuestionId == request.QuestionId &&
                                                                                                      x.AnswerId == request.AnswerId);

                                                var listAnswers = await _mockTestKeyCodeDataService.GetKeyCodeAnswerAsync(request.KeyCode);
                                                var redisMockTestKeyCodeAnswerData = listAnswers.FirstOrDefault(x => x.KeyCode == request.KeyCode &&
                                                                                                      x.MockTestSectionId ==
                                                                                                      request.MockTestSectionId &&
                                                                                                      x.MockTestPartId == request.MockTestPartId &&
                                                                                                      x.QuestionnaireId ==
                                                                                                      request.QuestionnaireId &&
                                                                                                      x.QuestionId == request.QuestionId &&
                                                                                                      x.AnswerId == request.AnswerId);
                                                if (request.MatchingQuestionId.HasValue && redisMockTestKeyCodeAnswerData != null)
                                                {
                                                    await _mongoMockTestKeyCodeAnswer.InsertOneAsync(redisMockTestKeyCodeAnswerData);
                                                }

                                                break;
                                            }

                                    }

                                    //update UpdateMockTestMenu
                                    var redisMockTestKeyCodeData = await _mockTestKeyCodeDataService.GetMockTestKeyCodeAsync(request.KeyCode);
                                    //add data to mongo
                                    var mongoMockTestKeyCodeData = await _mongoMockTestKeyCodeService.FindByIdAsync(p => p.KeyCode == request.KeyCode);
                                    if (mongoMockTestKeyCodeData == null)
                                    {
                                        await _mongoMockTestKeyCodeService.InsertOneAsync(redisMockTestKeyCodeData);
                                    }
                                    else
                                    {
                                        redisMockTestKeyCodeData.Id = mongoMockTestKeyCodeData.Id;
                                        await _mongoMockTestKeyCodeService.ReplaceOneAsync(redisMockTestKeyCodeData);
                                    }
                                    _logger.LogInformation($"MockTestWrapperKeyCode_SaveAnswer ---------- keycode: {request.KeyCode} --- questionId: {request.QuestionId} ----- End ");


                                    break;
                                }
                            case RabbitMQMockTestWrapperKeyCodeAction.MarkQuestion:
                                {


                                    var request = JsonConvert.DeserializeObject<MarkQuestionRequest>(message.Data);
                                    var _mongoMockTestKeyCodeService = scope.ServiceProvider.GetRequiredService<IMongoGenericRepository<MgMockTestKeyCodeModel>>();
                                    var _mockTestKeyCodeDataService = scope.ServiceProvider.GetRequiredService<IMockTestKeyCodeRedisDataService>();

                                    _logger.LogInformation($"MockTestWrapperKeyCode_MarkQuestion ---------- keycode: {request.KeyCode} --- QuestionnaireId: {request.QuestionnaireId} ----- Start ");

                                    //get data from redis
                                    var redisMockTestKeyCodeData = await _mockTestKeyCodeDataService.GetMockTestKeyCodeAsync(request.KeyCode);

                                    //add data to mongo
                                    var mongoMockTestKeyCodeData = await _mongoMockTestKeyCodeService.FindByIdAsync(p => p.KeyCode == request.KeyCode);
                                    if (mongoMockTestKeyCodeData == null)
                                    {
                                        await _mongoMockTestKeyCodeService.InsertOneAsync(redisMockTestKeyCodeData);
                                    }
                                    else
                                    {
                                        redisMockTestKeyCodeData.Id = mongoMockTestKeyCodeData.Id;
                                        await _mongoMockTestKeyCodeService.ReplaceOneAsync(redisMockTestKeyCodeData);
                                    }

                                    _logger.LogInformation($"MockTestWrapperKeyCode_MarkQuestion ---------- keycode: {request.KeyCode} --- QuestionnaireId: {request.QuestionnaireId} ----- End ");

                                    break;
                                }

                            case RabbitMQMockTestWrapperKeyCodeAction.Delete:
                                {
                                    _logger.LogInformation($"MockTestWrapperKeyCode_Delete ---------- keycode: {message.KeyCode} --- Start");

                                    var _mongoMockTestKeyCodeService = scope.ServiceProvider.GetRequiredService<IMongoGenericRepository<MgMockTestKeyCodeModel>>();
                                    var _mongoMockTestKeyCodeAnswerService = scope.ServiceProvider.GetRequiredService<IMongoGenericRepository<MgKeyCodeAnswerModel>>();
                                    var _mockTestKeyCodeDataService = scope.ServiceProvider.GetRequiredService<IMockTestKeyCodeRedisDataService>();
                                    //delete from mongo
                                    await _mongoMockTestKeyCodeService.DeleteManyAsync(p => p.KeyCode == message.KeyCode);
                                    await _mongoMockTestKeyCodeAnswerService.DeleteManyAsync(p => p.KeyCode == message.KeyCode);

                                    //await _mockTestKeyCodeDataService.ClearCountMockTestKeyCodeExamining();

                                    _logger.LogInformation($"MockTestWrapperKeyCode_Delete ---------- keycode: {message.KeyCode} --- End");

                                    break;
                                }
                            default:
                                {
                                    break;
                                }
                        }
                        await uow.CompleteAsync();

                    }
                    _channel.BasicAck(eventArgs.DeliveryTag, false);


                }
            };
            string queueName = $"{Core.Common.ConfigureModels.Constants.RabbitMQ.MockTestWrapperKeyCode.QueueMockTestRedisToMongo}";
            _channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            if (_channel?.IsOpen == true)
                _channel.Close();

            _channel?.Dispose();
        }
    }
}

