using IIG.Core.Common.Models.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IIG.Application.Services
{
    public interface IFileUploaderService
    {
        Task<FileInsertModel> FileUploadAsync(IFormFile file, Guid itemId, EFileTypeIdentifier identifier, bool isPublic);
        Task<List<ImageInfoModel>> CreateScaledImagesAsync(IFormFile file, Guid itemId, EFileTypeIdentifier identifier, bool isPublic);
        Task<FileContentResult> DownLoadFileAsync(Guid id);
        Task<List<FileInsertModel>> StreamingUploadAsync(Guid itemId, EFileTypeIdentifier identifier, bool isPublic);
        Task<FileStreamResult> StreamingPlayAsync(Guid fileId);
        Task<byte[]> GetFileContentByFileIdAsync(Guid fileId);
        Task<FileMigrateModel> GetInfoMigrateFileAsync(Guid fileId);
    }
}
