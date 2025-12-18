using IIG.Application.Data;
using IIG.Core.Common.Models.Files;
using IIG.Core.Services.Interfaces;

namespace IIG.Application.Services
{
    public class FileTypeService : IFileTypeService
    {
        private readonly IFileTypeDA _fileTypeDA;

        public FileTypeService(IFileTypeDA fileTypeDA )
        {
            _fileTypeDA = fileTypeDA;
        }

        public async Task<FileTypeModel> GetByIdentifier(EFileTypeIdentifier identifier)
        {
            return await _fileTypeDA.GetByIdentifier(identifier);
        }
    }
}
