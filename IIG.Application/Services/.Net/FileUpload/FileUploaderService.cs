using IIG.Core.Common.ConfigureModels;
using IIG.Core.Common.ErrorHandling;
using IIG.Core.Common.Models.Files;
using IIG.Core.Helpers;
using IIG.Core.Providers.Caching;
using IIG.Core.Services.Interfaces;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using IIG.Core.Services;
using IIG.Core.Repository;
using IIG.Core.Entities;


namespace IIG.Application.Services
{
    public class FileUploaderService : IFileUploaderService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<FileUploaderService> _logger;
        private readonly IRepository<Core.Entities.File> _fileRepos;
        private readonly IStorageService _storageService;
        private readonly IFileTypeService _fileTypeService;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDistributedCacheProvider _distributedCacheProvider;
        private readonly ISwiftStorageService _swiftStorageService;

        public FileUploaderService(
            IConfiguration configuration,
            ILogger<FileUploaderService> logger,
            IRepository<Core.Entities.File> fileRepos,
            IStorageService storageService,
            IFileTypeService fileTypeService,
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor, IDistributedCacheProvider distributedCacheProvider,
            ISwiftStorageService swiftStorageService)
        {
            _config = configuration;
            _logger = logger;
            _fileRepos = fileRepos;
            _storageService = storageService;
            _fileTypeService = fileTypeService;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
            _distributedCacheProvider = distributedCacheProvider;
            _swiftStorageService = swiftStorageService;
        }

        public async Task<List<ImageInfoModel>> CreateScaledImagesAsync(IFormFile file, Guid itemId, EFileTypeIdentifier identifier, bool isPublic)
        {
            if (file == null) throw new ApiValidationException(Constants.FileSettings.FileIsEmptyKey, nameof(file));

            if (!Constants.FileSettings.ListImageType.Any(x => x == identifier)) return null;

            _logger.LogInformation($"Create Scaled Images For: {file.FileName}");

            try
            {
                var fileType = await _fileTypeService.GetByIdentifier(identifier);
                FileHelper.IsValidFile(file, fileType);

                int originalHeight;
                int originalWidth;

                using var imageMagick = new MagickImage(file.OpenReadStream());
                originalHeight = imageMagick.Height;
                originalWidth = imageMagick.Width;

                List<ImageInfoModel> result = new();
                var isGifFile = Path.GetExtension(file.FileName) == ".gif";
                if (isGifFile)
                {
                    var itemSizeType = EImageSizeType.Original;
                    var (height, width) = ScaledImageDimensions(itemSizeType, originalWidth, originalHeight);

                    var imageUri = await UploadScaledImageAsync(file, itemId, width, height, itemSizeType, fileType, isPublic);
                    result.Add(imageUri);
                }
                else
                {
                    foreach (EImageSizeType itemSizeType in Enum.GetValues(typeof(EImageSizeType)))
                    {
                        var (height, width) = ScaledImageDimensions(itemSizeType, originalWidth, originalHeight);

                        if (originalHeight < height || originalWidth < width)
                        {
                            continue;
                        }

                        var imageUri = await UploadScaledImageAsync(file, itemId, width, height, itemSizeType, fileType, isPublic);
                        result.Add(imageUri);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{itemId}");
                throw;
            }
        }

        private async Task<ImageInfoModel> UploadScaledImageAsync(IFormFile file, Guid itemId,
            int width, int height, EImageSizeType itemSizeType, FileTypeModel fileType, bool isPublic)
        {
            using var stream = file.OpenReadStream();

            var isGifFile = Path.GetExtension(file.FileName) == ".gif";
            var imageFileSetting = new MagickReadSettings
            {
                Format = isGifFile ? MagickFormat.Gif : MagickFormat.Unknown
            };

            var resultStream = new MemoryStream();

            if (isGifFile)
            {
                return await ImageUploadAsync(itemId, itemSizeType, file, stream, fileType, isPublic);
            }
            else
            {
                using (var imageMagick = new MagickImage(stream))
                {
                    if (width == 0)
                    {
                        return await ImageUploadAsync(itemId, itemSizeType, file, stream, fileType, isPublic);
                    }

                    var size = new MagickGeometry(width, height);
                    size.IgnoreAspectRatio = false;
                    imageMagick.Resize(size);

                    await imageMagick.WriteAsync(resultStream);
                }
            }

            resultStream.Position = 0;
            return await ImageUploadAsync(itemId, itemSizeType, file, resultStream, fileType, isPublic);
        }

        private async Task<ImageInfoModel> ImageUploadAsync(Guid itemId,
          EImageSizeType itemSizeType,
          IFormFile file,
          Stream stream,
          FileTypeModel fileType,
          bool isPublic)
        {
            string originFileName = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            string fileNamePattern = fileType.FileNamePattern
                .Replace(Constants.FileSettings.KeyReplaceId, itemId.ToString())
                .Replace(Constants.FileSettings.KeyReplaceUserInput,
                    itemSizeType == EImageSizeType.Original
                        ? originFileName.ConvertNonASCII()
                        : $"{originFileName.ConvertNonASCII()}_{itemSizeType.ToString().ToLower()}")
                .Replace(Constants.FileSettings.KeyReplaceTimestamp,
                    DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString());
            string fileName = fileNamePattern + extension;

            var moment = DateTime.UtcNow;
            string folderPath = fileType.DefaultStorageLocation
                .Replace(Constants.FileSettings.KeyReplaceYear, moment.Year.ToString())
                .Replace(Constants.FileSettings.KeyReplaceMonth, moment.Month.ToString())
                .Replace(Constants.FileSettings.KeyReplaceId, itemId.ToString());

            var uri = await _storageService.Upload(new StorageInfo
            {
                FileName = fileName,
                IsPublic = isPublic,
                StorageFolder = folderPath,
                Stream = stream
            });

            ImageInfoModel result = new()
            {
                FileName = uri,
                DisplayName = originFileName,
                StorageLocation = folderPath,
                Extension = extension,
                FileSize = file.Length,
                Uri = uri,
                SizeType = itemSizeType,
                FileTypeId = fileType.Id,
                ItemId = itemId,
                EntityFile = EntityFileTypeMapping(fileType),
                IsPublic = isPublic,
                IsMigrate = true
            };

            _logger.LogInformation("Upload File is successfully");
            return result;
        }
        private static EEntityFile? EntityFileTypeMapping(FileTypeModel fileType)
        {
            switch (fileType.Identifier)
            {
                case EFileTypeIdentifier.ImageCourse:
                    return EEntityFile.Course;

                case EFileTypeIdentifier.ImageUser:
                    return EEntityFile.WebUser;

                case EFileTypeIdentifier.ImageCourseTeacher:
                    return EEntityFile.CourseTeacher;

                case EFileTypeIdentifier.ImageNews:
                    return EEntityFile.News;

                default:
                    return null;
            }
        }
        private static (int height, int width) ScaledImageDimensions(EImageSizeType itemSizeType, int originalWidth, int originalHeight)
        {
            int height;
            int width;
            switch (itemSizeType)
            {
                case EImageSizeType.Large:
                    width = 2048;
                    height = 2048;
                    break;
                case EImageSizeType.Medium:
                    width = 1024;
                    height = 1024;
                    break;
                case EImageSizeType.Small:
                    width = 512;
                    height = 512;
                    break;
                case EImageSizeType.Thumbnail:
                    width = 266;
                    height = 177;
                    break;
                default:
                    width = originalWidth;
                    height = originalHeight;
                    break;
            }

            return (height, width);
        }

        public async Task<FileInsertModel> FileUploadAsync(IFormFile file, Guid itemId, EFileTypeIdentifier identifier, bool isPublic)
        {
            if (file == null) throw new ApiValidationException(Constants.FileSettings.FileIsEmptyKey, nameof(file));

            if (Constants.FileSettings.ListImageType.Any(x => x == identifier)) return null;

            _logger.LogInformation($"Started uploading file: {file.FileName}");
            try
            {
                var fileType = await _fileTypeService.GetByIdentifier(identifier);
                FileHelper.IsValidFile(file, fileType);

                await using var stream = (Stream)new MemoryStream();
                await file.CopyToAsync(stream);

                _logger.LogInformation($"Upload file is successfully");
                return await HandleStoreIntoPhysical(stream, file.FileName, fileType, itemId, isPublic);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{itemId}");
                throw;
            }
        }

        public async Task<FileContentResult> DownLoadFileAsync(Guid id)
        {
            var fileInfo = await _fileService.GetByIdAsync(id);
            if (fileInfo == null) return null;

            try
            {
                var fileName = Path.GetFileName(fileInfo.FileName);
                var contentType = FileHelper.GetMimeType(fileName);
                var content = await _storageService.GetBytes(fileInfo.FileName, fileInfo.IsPublic);

                return new FileContentResult(content, contentType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Dowload file error");
                return null;
            }
        }

        public async Task<List<FileInsertModel>> StreamingUploadAsync(Guid itemId, EFileTypeIdentifier identifier, bool isPublic)
        {


            if (Constants.FileSettings.ListImageType.Any(x => x == identifier)) return null;

            var contextRequest = _httpContextAccessor.HttpContext.Request;
            if (!MultipartRequestHelper.IsMultipartContentType(_httpContextAccessor.HttpContext.Request.ContentType))
            {
                throw new ApiValidationException("The request couldn't be processed (Error 1).", "File");
            }

            var multipartBoundaryLengthLimit = 2097152;
            var boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(contextRequest.ContentType), multipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, contextRequest.Body);
            var section = await reader.ReadNextSectionAsync();

            var fileType = await _fileTypeService.GetByIdentifier(identifier);
            var result = new List<FileInsertModel>();

            while (section != null)
            {
                var hasContentDispositionHeader =
                    ContentDispositionHeaderValue.TryParse(
                        section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
                    if (!MultipartRequestHelper
                        .HasFileContentDisposition(contentDisposition))
                    {
                        throw new ApiValidationException("The request couldn't be processed (Error 2).", "File");
                    }
                    else
                    {
                        //will write file directly into folder instead of using memorystream (out of memory), revise later
                        //using (var targetStream = System.IO.File.Create(saveToPath))
                        //{
                        //    await section.Body.CopyToAsync(targetStream);
                        //}

                        //using var streamedFileContent = new MemoryStream();
                        //await section.Body.CopyToAsync(streamedFileContent);
                        //FileHelper.IsValidFile(streamedFileContent.Length, contentDisposition.FileName.Value, fileType);

                        //result.Add(await HandleStoreIntoPhysical(streamedFileContent, contentDisposition.FileName.Value, fileType, itemId, isPublic));

                        FileHelper.IsValidFile(1024, contentDisposition.FileName.Value, fileType);
                        result.Add(await HandleStoreIntoPhysical(section.Body, contentDisposition.FileName.Value, fileType, itemId, isPublic));
                    }
                }

                section = await reader.ReadNextSectionAsync();
            }

            return result;
        }

        private async Task<FileInsertModel> HandleStoreIntoPhysical(Stream streamedFileContent, string fileName,
            FileTypeModel fileType, Guid itemId, bool isPublic)
        {

            string originFileName = Path.GetFileNameWithoutExtension(fileName);
            string extension = Path.GetExtension(fileName);
            string fileNamePattern = fileType.FileNamePattern
                .Replace(Constants.FileSettings.KeyReplaceId, itemId.ToString())
                .Replace(Constants.FileSettings.KeyReplaceUserInput, originFileName.ConvertNonASCII())
                .Replace(Constants.FileSettings.KeyReplaceTimestamp, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString());
            fileName = fileNamePattern + extension;

            var moment = DateTime.UtcNow;
            string folderPath = fileType.DefaultStorageLocation
                .Replace(Constants.FileSettings.KeyReplaceYear, moment.Year.ToString())
                .Replace(Constants.FileSettings.KeyReplaceMonth, moment.Month.ToString())
                .Replace(Constants.FileSettings.KeyReplaceId, itemId.ToString());

            var uri = await _storageService.Upload(new StorageInfo
            {
                FileName = fileName,
                IsPublic = isPublic,
                StorageFolder = folderPath,
                Stream = streamedFileContent
            });


            return new FileInsertModel
            {
                FileName = uri,
                DisplayName = originFileName,
                StorageLocation = folderPath,
                Extension = extension,
                FileSize = streamedFileContent.Length,
                FileTypeId = fileType.Id,
                IsActive = true,
                IsPublic = isPublic,
                IsMigrate = true,
            };
        }

        public async Task<FileStreamResult> StreamingPlayAsync(Guid fileId)
        {
            var fileInfo = await _distributedCacheProvider
                .Cache<FileInfoCachingModel>()
                .GetOrAdd(string.Format(Constants.RedisKey.RedisFileInfo, fileId),
                    async () => await GetFileInfoForCachingAsync(fileId));
            if (fileInfo == null) return null;

            var stream = System.IO.File.OpenRead(fileInfo.Url);
            return new FileStreamResult(stream, fileInfo.ContentType)
            {
                EnableRangeProcessing = true
            };
        }

        public async Task<byte[]> GetFileContentByFileIdAsync(Guid fileId)
        {
            return await _fileService.GetFileContentByIdAsync(fileId);
        }

        private async Task<FileInfoCachingModel> GetFileInfoForCachingAsync(Guid fileId)
        {
            var fileModel = await _fileService.GetByIdAsync(fileId);
            if (fileModel == null) return null;

            var fileName = Path.GetFileName(fileModel.FileName);
            var contentType = FileHelper.GetMimeType(fileName);
            string rootFolder = AppDomain.CurrentDomain.BaseDirectory.EnsureEndWithSlash() + Constants.FileSettings.RootPathFileStorage.EnsureEndWithSlash();
            if (fileModel.IsPublic)
            {
                rootFolder += (_config["vngConfig:Bucket:BUCKET_PUBLIC"] == null ? Constants.FileSettings.SubFolderPublic : _config["vngConfig:Bucket:BUCKET_PUBLIC"]).EnsureEndWithSlash();
            }
            else
            {
                rootFolder += (_config["vngConfig:Bucket:BUCKET_PRIVATE"] == null ? Constants.FileSettings.SubFolderPrivate : _config["vngConfig:Bucket:BUCKET_PRIVATE"]).EnsureEndWithSlash();
            }
            var storageFolderPath = rootFolder;
            if (!string.IsNullOrEmpty(string.Empty))
            {
                storageFolderPath = $"{rootFolder}{string.Empty.EnsureEndWithSlash()}";
                if (!Directory.Exists(string.Empty))
                {
                    Directory.CreateDirectory(storageFolderPath);
                }
            }

            return new FileInfoCachingModel
            {
                Id = fileModel.Id,
                FileName = fileModel.FileName,
                ContentType = contentType,
                Url = Path.Combine(storageFolderPath, fileName)
            };
        }

        public async Task<FileMigrateModel> GetInfoMigrateFileAsync(Guid fileId)
        {
            var fileModel = await _fileRepos.GetAll().Where(x => x.Id == fileId).Select(f => new
            {
                IsMigrate = f.IsMigrate,
                IsPublic = f.IsPublic,
                FileName = f.FileName
            }).FirstOrDefaultAsync();

            if (fileModel == null) return null;

            if (fileModel.IsMigrate != true)
                return new FileMigrateModel
                {
                    IsMigrate = fileModel.IsMigrate,
                };

            var bucket = _config["vngConfig:Bucket:BUCKET_PRIVATE"] == null ? Constants.FileSettings.SubFolderPrivate : _config["vngConfig:Bucket:BUCKET_PRIVATE"];
            if (fileModel.IsPublic)
            {
                bucket = _config["vngConfig:Bucket:BUCKET_PUBLIC"] == null ? Constants.FileSettings.SubFolderPublic : _config["vngConfig:Bucket:BUCKET_PUBLIC"];
            }
            return await _swiftStorageService.GetTempUrlAsync(bucket, fileModel.FileName);
        }
    }
}
