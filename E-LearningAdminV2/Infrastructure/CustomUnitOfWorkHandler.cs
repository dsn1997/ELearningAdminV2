
using IIG.Core.Interface;
using System.Text;

namespace Backend.Infrastructure.Middleware
{
    public class CustomUnitOfWorkMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomUnitOfWorkMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUnitOfWorkManager unitOfWorkManager)
        {
            var originalBodyStream = context.Response.Body;
            await using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;
            await using (var uow = unitOfWorkManager.Begin())
            {
                try
                {
                    await _next(context);

                    if (context.Response.StatusCode < 400 && !context.Items.ContainsKey("HasError"))
                    {
                        await uow.CompleteAsync();
                    }
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    await memoryStream.CopyToAsync(originalBodyStream);
                }
                catch (Exception ex)
                {

                    context.Response.Clear();
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    var error = new
                    {
                        message = ex.Message,
                        detail = ex.InnerException?.Message
                    };

                    var json = System.Text.Json.JsonSerializer.Serialize(error);
                    var errorBytes = Encoding.UTF8.GetBytes(json);
                    await originalBodyStream.WriteAsync(errorBytes, 0, errorBytes.Length);
                }
                finally
                {
                    context.Response.Body = originalBodyStream;
                }
            }

              
        }
    }



}
