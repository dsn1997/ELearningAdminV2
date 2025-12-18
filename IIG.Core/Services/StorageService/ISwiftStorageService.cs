using Amazon.S3.Model;
using IIG.Core.Common.Models.Files;

namespace IIG.Core.Services
{
    public interface ISwiftStorageService
    {
        // get temp url for file
        Task<FileMigrateModel> GetTempUrlAsync(string container, string fileName);
        Task<AuthToken> GetTempUrlAsync();
        Task<bool> CheckFileExistAsync(string container, string fileName);
        Task<Byte[]> GetBytes(string container, string fileName);
        Task<PutObjectResponse> S3UploadAsync(string container, string filePathS3, Stream data, string contentType);
    }
}