using IIG.Core.Common.Models.Files;

namespace IIG.Core.Services
{
    public interface IStorageService
    {
        Task<string> Upload(StorageInfo info);
        Task DeleteFile(string storageFolder, string fileName, bool isPublic = true);
        Task DeleteFile(string filePath, bool isPublic = true);
        Task DeleteFolder(string storageFolder, bool isPublic = true);
        Task<byte[]> GetBytes(string filePath, bool isPublic = true);
        string BuildPath(string storageFolder = "", string fileName = "", bool isPublic = true);
        bool ExistsFileByPath(string path);
        bool ExistsFilePublicByPath(string path);
    }
}
