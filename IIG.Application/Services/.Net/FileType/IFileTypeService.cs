using IIG.Core.Common.Models.Files;

namespace IIG.Application.Services
{
    public interface IFileTypeService
    {
        Task<FileTypeModel> GetByIdentifier(EFileTypeIdentifier identifier);
    }
}
