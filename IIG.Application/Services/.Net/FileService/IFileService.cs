using IIG.Core.Common.Models.Files;

namespace IIG.Application.Services;
public interface IFileService
{
    Task InsertFileAsync(FileInsertModel model);
    Task UpdateFileAsync(FileInsertModel model);
    Task DeleteFileAsync(Guid fileId);
    Task DeletePhysicalFileAsync(FileModel model);
    Task<FileModel> GetByIdInternalAsync(Guid id);
    Task<FileModel> GetByIdAsync(Guid id);
    Task<FileDto> GetDtoByIdAsync(Guid id);
    Task<byte[]> GetFileContentByIdAsync(Guid id);
    Task<Dictionary<Guid, int>> GetFilesDuration(List<Guid> fileIds);
}
