using System.Text;
using Amazon.S3;
using Amazon.S3.Model;
using IIG.Core.Common.Models.Files;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IIG.Core.Services;

public class SwiftStorageService : ISwiftStorageService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<SwiftStorageService> _logger;
    private readonly AmazonS3Client _s3Client;
    public SwiftStorageService(IConfiguration configuration, ILogger<SwiftStorageService> logger)
    {
        _configuration = configuration;
        _logger = logger;
        AmazonS3Config config = new AmazonS3Config();
        config.ServiceURL = _configuration["VngConfig:SERVICE_URL"];
        config.ForcePathStyle = true;
        config.AuthenticationRegion = _configuration["VngConfig:AUTHENTICATION_REGION"];
        _s3Client = new AmazonS3Client(_configuration["VngConfig:ACCESS_KEY"], _configuration["VngConfig:SECRET_KEY"], config);
    }

    public async Task<AuthToken> GetTempUrlAsync()
    {
        var username = _configuration["VngConfig:USER_NAME"];
        var password = _configuration["VngConfig:PASSWORD"];
        var authUrl = _configuration["VngConfig:AUTHENTICATION_URL"];
        var projectId = _configuration["VngConfig:PROJECT_ID"];
        var jsonObject = new
        {
            auth = new
            {
                scope = new
                {
                    project = new
                    {
                        id = projectId,
                        domain = new
                        {
                            name = "default"
                        }
                    }
                },
                identity = new
                {
                    methods = new[] { "password" },
                    password = new
                    {
                        user = new
                        {
                            name = username,
                            password,
                            domain = new
                            {
                                name = "default"
                            }
                        }
                    }
                }
            }
        };
        string jsonString = JsonConvert.SerializeObject(jsonObject);
        using HttpClient httpClient = new();
        try
        {
            StringContent content = new(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(authUrl + "/auth/tokens", content);

            if (response.IsSuccessStatusCode)
            {
                var responseJson = JsonConvert.DeserializeObject<AuthTokenVngDto>(await response.Content.ReadAsStringAsync());
                // Get the x-subject-token header
                if (response.Headers.TryGetValues("x-subject-token", out var subjectTokenValues))
                {
                    foreach (var subjectTokenValue in subjectTokenValues)
                    {

                        return new AuthToken
                        {
                            Token = subjectTokenValue,
                            TempUrl = responseJson?.Token?.Catalog?.FirstOrDefault()?.Endpoints?.FirstOrDefault()?.Url
                        };
                    }
                }
                else
                {
                    _logger.LogError("x-subject-token header not found.");

                }
            }
            else
            {
                _logger.LogError("Request failed with status code: " + response.StatusCode);
            }
        }
        catch (HttpRequestException e)
        {
            _logger.LogError("Request exception: " + e.Message);
        }

        return null;
    }

    public async Task<FileMigrateModel> GetTempUrlAsync(string container, string fileName)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = container,
            Key = fileName,
            Expires = DateTime.UtcNow.AddHours(12),
            Protocol = Protocol.HTTPS
        };

        var url = _s3Client.GetPreSignedURL(request);
        await Task.CompletedTask;
        return new FileMigrateModel
        {
            IsMigrate = true,
            TempUrl = url
        };
    }

    public async Task<PutObjectResponse> S3UploadAsync(string container, string filePathS3, Stream data, string contentType)
    {

        _logger.LogInformation("File size: {Size} bytes", data.Length);
        _logger.LogInformation("Content-Type: {ContentType}", contentType);
        _logger.LogInformation("S3 target key: {S3Key}", filePathS3);

        if (!data.CanRead)
            throw new InvalidOperationException("Stream is not readable");

        // Create a new MemoryStream if the original stream isn't seekable
        Stream uploadStream = data;
        if (!data.CanSeek)
        {
            _logger.LogWarning("Original stream is not seekable, copying to MemoryStream");
            var memoryStream = new MemoryStream();
            await data.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            uploadStream = memoryStream;
        }
        else if (data.Position > 0)
        {
            data.Position = 0;
        }

        var request = new PutObjectRequest
        {
            BucketName = container,
            Key = filePathS3,
            InputStream = uploadStream,
            ContentType = contentType,
            AutoCloseStream = true // Closes the stream after upload
        };
        try
        {
            var response = await _s3Client.PutObjectAsync(request);
            _logger.LogInformation("File uploaded successfully to S3: {Bucket}/{Key}", container, filePathS3);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to upload file to S3");
            throw;
        }
    }

    public async Task<bool> CheckFileExistAsync(string container, string fileName)
    {
        try
        {
            var request = new GetObjectMetadataRequest
            {
                BucketName = container,
                Key = fileName
            };
            await _s3Client.GetObjectMetadataAsync(request);
            return true;
        }
        catch (AmazonS3Exception e)
        {
            if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
            _logger.LogError("Error check file exist: " + e.Message);
            throw;
        }
    }

    public async Task<Byte[]> GetBytes(string container, string fileName)
    {
        try
        {
            var request = new GetObjectRequest
            {
                BucketName = container,
                Key = fileName
            };
            using var response = await _s3Client.GetObjectAsync(request);
            using var responseStream = response.ResponseStream;
            using var memoryStream = new MemoryStream();
            await responseStream.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
        catch (AmazonS3Exception e)
        {
            _logger.LogError("Error get bytes: " + e.Message);
            throw;
        }
    }
}