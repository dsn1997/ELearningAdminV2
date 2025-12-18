using IIG.Core.Common.Models.Files;

namespace IIG.Application.Data
{
    public interface IFileTypeDA 
    {
        Task<FileTypeModel> GetByIdentifier(EFileTypeIdentifier identifier);
        Task<FileTypeModel> GetByIdAsync(Guid id);
    }
}
