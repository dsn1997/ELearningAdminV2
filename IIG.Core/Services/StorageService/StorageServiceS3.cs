using System.Net.Http.Headers;
using IIG.Core.Common.ConfigureModels;
using IIG.Core.Common.Models.Files;
using IIG.Core.Helpers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace IIG.Core.Services
{
    public class StorageServiceS3 : IStorageService
    {
        private readonly ILogger<StorageServiceS3> _logger;
        private readonly ISwiftStorageService _swiftStorageService;
        private readonly IConfiguration _config;

        public StorageServiceS3(IConfiguration configuration, ILogger<StorageServiceS3> logger, ISwiftStorageService swiftStorageService)
        {
            _config = configuration;
            _logger = logger;
            _swiftStorageService = swiftStorageService;
        }

        public async Task<string> Upload(StorageInfo info)
        {
            info.Stream.Position = 0;
            var bucket = _config["vngConfig:Bucket:BUCKET_PRIVATE"] == null ? Constants.FileSettings.SubFolderPrivate : _config["vngConfig:Bucket:BUCKET_PRIVATE"];
            if (info.IsPublic)
            {
                bucket = _config["vngConfig:Bucket:BUCKET_PUBLIC"] == null ? Constants.FileSettings.SubFolderPublic : _config["vngConfig:Bucket:BUCKET_PUBLIC"];
            }
            try
            {
                var mimeType = GetMimeTypeFromExtension(info.FileName);
                var filePathS3 = BuildPathS3NonBucket(info.StorageFolder, info.FileName);
                _logger.LogInformation($"filePath s3 : {filePathS3}, isPublic: {info.IsPublic}");

                var responseUpload = await _swiftStorageService.S3UploadAsync(bucket, filePathS3, info.Stream, mimeType);
                _logger.LogInformation($"response s3 : {responseUpload}");

                if (responseUpload?.HttpStatusCode >= System.Net.HttpStatusCode.OK &&
                   responseUpload?.HttpStatusCode < System.Net.HttpStatusCode.Ambiguous)
                {
                    return Path.Combine(info.StorageFolder, info.FileName);
                }

                var errorMessage = responseUpload;
                _logger.LogError("Error upload file to S3" + errorMessage);
            }
            catch (Exception e)
            {
                _logger.LogError("Error upload file to S3" + e.Message);
                throw;
            }

            return null;
        }

        public Task DeleteFile(string storageFolder, string fileName, bool isPublic = true)
        {
            var filePath = BuildPath(storageFolder, fileName, isPublic);
            return DeleteFile(filePath);
        }

        public Task DeleteFile(string filePath, bool isPublic = true)
        {
            return DeleteFolder(filePath, isPublic);
        }

        public async Task DeleteFolder(string storageFolder, bool isPublic = true)
        {
            try
            {
                // get auth token S3
                var tempUrl = await _swiftStorageService.GetTempUrlAsync();

                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("X-Auth-Token", tempUrl.Token);

                var filePath = BuildPath(storageFolder, string.Empty, isPublic);

                var responseUpload = await httpClient.DeleteAsync(Path.Combine(tempUrl.TempUrl, filePath));

                if (!responseUpload.IsSuccessStatusCode)
                {
                    string errorMessage = await responseUpload.Content.ReadAsStringAsync();
                    _logger.LogError("Error deleting folder in S3: " + errorMessage);
                    throw new Exception("Error deleting folder in S3: " + errorMessage);
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error deleting folder in S3: " + e.Message);
                throw;
            }
        }

        public string BuildPath(string storageFolder = "", string fileName = "", bool isPublic = true)
        {
            string filePath = Path.Combine(storageFolder, fileName);
            if (isPublic)
            {
                filePath = Path.Combine((_config["vngConfig:Bucket:BUCKET_PUBLIC"] == null ? Constants.FileSettings.SubFolderPublic : _config["vngConfig:Bucket:BUCKET_PUBLIC"]).EnsureEndWithSlash(), filePath);
            }
            else
            {
                filePath = (_config["vngConfig:Bucket:BUCKET_PRIVATE"] == null ? Constants.FileSettings.SubFolderPrivate : _config["vngConfig:Bucket:BUCKET_PRIVATE"]).EnsureEndWithSlash() + filePath;
            }
            return filePath;
        }


        public string BuildPathS3NonBucket(string storageFolder = "", string fileName = "")
        {
            return Path.Combine(storageFolder, fileName);
        }

        public bool ExistsFileByPath(string path)
        {
            var isExist = _swiftStorageService.CheckFileExistAsync((_config["vngConfig:Bucket:BUCKET_PRIVATE"] == null ? Constants.FileSettings.SubFolderPrivate : _config["vngConfig:Bucket:BUCKET_PRIVATE"]), path);
            return isExist.Result;
        }

        public bool ExistsFilePublicByPath(string path)
        {
            var isExist = _swiftStorageService.CheckFileExistAsync((_config["vngConfig:Bucket:BUCKET_PUBLIC"] == null ? Constants.FileSettings.SubFolderPublic : _config["vngConfig:Bucket:BUCKET_PUBLIC"]), path);
            return isExist.Result;
        }

        public Task<byte[]> GetBytes(string filePath, bool isPublic = true)
        {
            return _swiftStorageService.GetBytes((_config["vngConfig:Bucket:BUCKET_PUBLIC"] == null ? Constants.FileSettings.SubFolderPublic : _config["vngConfig:Bucket:BUCKET_PUBLIC"]), filePath);
        }

        private static string GetMimeTypeFromExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName)?.ToLower();

            return extension switch
            {
                // audio
                ".mp3" => "audio/mp3",
                // video
                ".mp4" => "video/mp4",
                ".avi" => "video/x-msvideo",
                ".mov" => "video/quicktime",
                ".wmv" => "video/x-ms-wmv",
                // file
                ".pdf" => "application/pdf",
                ".txt" or ".srt" => "text/plain",
                ".doc" or ".docx" => "application/msword",
                // image
                ".jpg" or ".jpeg" or ".jpe" => "image/jpeg",
                ".png" => "image/png",
                ".svg" => "image/svg+xml",
                ".gif" => "image/gif",
                _ => "application/octet-stream",
            };
        }

    }
}
