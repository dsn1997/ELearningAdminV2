using IIG.Core.Common.Models.Files;

namespace IIG.Application.Data;
public interface IFileDA 
{
    Task InsertFileAsync(FileInsertModel model);
    Task UpdateFileAsync(FileInsertModel model);
    Task DeleteFileAsync(Guid fileId);
    Task<FileModel> GetByIdAsync(Guid id);
    Task<Dictionary<Guid, int>> GetFilesDuration(List<Guid> fileIds);
}