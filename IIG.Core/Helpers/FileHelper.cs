using IIG.Core.Common.ConfigureModels;
using IIG.Core.Common.ErrorHandling;
using IIG.Core.Common.Models.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;


namespace IIG.Core.Helpers
{
    public static class FileHelper
    {
        public static void IsValidFile(IFormFile file, FileTypeModel filetype)
        {
            IsValidFile(file.Length, file.FileName, filetype);
        }
        public static void IsValidFile(long fileLength, string fileName, FileTypeModel filetype)
        {
            if (fileLength == 0) throw new ApiValidationException(Constants.FileSettings.FileIsEmptyKey, nameof(fileLength));

            if (filetype == null) throw new ApiValidationException(Constants.FileSettings.FileTypeIsNotConfiguredKey, nameof(filetype));

            if (string.IsNullOrEmpty(filetype.FileExtensions)) throw new ApiValidationException(Constants.FileSettings.FileTypeIsNotConfiguredKey, nameof(FileTypeModel.FileExtensions));

            string extension = Path.GetExtension(fileName);
            string[] extensionConfigs = filetype.FileExtensions.Split(';');

            if (!extensionConfigs.Contains(extension, StringComparer.OrdinalIgnoreCase))
            {
                throw new ApiValidationException(string.Format(Constants.FileSettings.FileExtensionNotValid, filetype.Name, string.Join("_", extensionConfigs))
                    .Replace(" ", "_"), nameof(extension));
            }

            //Megabyte unit
            var maxSizeByte = filetype.MaxSize * 1024 * 1024;
            if (fileLength > maxSizeByte)
            {
                throw new ApiValidationException(string.Format(Constants.FileSettings.FileSizeNotValid, filetype.Name, filetype.MaxSize)
                    .Replace(" ", "_"), nameof(maxSizeByte));
            }
        }

        public static string GetMimeType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(fileName, out string contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }

        public static bool ValidateFileFromUrl(string url, ETypeOfFile eTypeOfFile)
        {
            var dicExtension = new Dictionary<ETypeOfFile, string>()
            {
                { ETypeOfFile.Image, ".jpg;.jpeg;.jpe;.png;.svg" },
                { ETypeOfFile.Audio, ".mp3;" },
                { ETypeOfFile.Video, ".mp4;.avi;.mov;.wmv" },
                { ETypeOfFile.Pdf, ".pdf" },
                { ETypeOfFile.Script, ".txt;.srt" },
            };

            string fileExtension = Path.GetExtension(url);
            string[] extensions = dicExtension[eTypeOfFile].Split(';');

            return extensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
        }
                     
        public static string UpdateLastSegmentToLower(string path)
        {
            var segments = path.Split('/');
            if (segments.Length == 0)
            {
                return path;
            }

            segments[^1] = Uri.EscapeDataString(segments[^1]);

            return string.Join("/", segments);
        }
    }
}

