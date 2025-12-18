using IIG.Core.Base;
using IIG.Core.Common.Models.Files;
using IIG.Core.Entities;
using IIG.Core.Helper;
using IIG.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IIG.Application.Data
{
    public class FileTypeDA : IFileTypeDA
    {

        private readonly IAppFactory _appFactory;
        private readonly IRepository<FileType> _fileTypeRepos;

        public FileTypeDA(IAppFactory appFactory,
            IRepository<FileType> fileTypeRepos)
        {
            _appFactory = appFactory;
            _fileTypeRepos = fileTypeRepos;
        }

        public async Task<FileTypeModel> GetByIdentifier(EFileTypeIdentifier identifier)
        {
            var dto = await _fileTypeRepos.GetAll()
                .Where(x => x.Identifier == (short)identifier)
                .Select(x => new FileTypeModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Identifier = (EFileTypeIdentifier)x.Identifier,
                    DefaultStorageLocation = x.DefaultStorageLocation,
                    FileExtensions = x.FileExtensions,
                    FileNamePattern = x.FileNamePattern,
                    MaxSize = x.MaxSize,
                })
                .FirstOrDefaultAsync();
            return dto;
        }

        public async Task<FileTypeModel> GetByIdAsync(Guid id)
        {
            var dto = await _fileTypeRepos.GetAll()
               .Where(x => x.Id == id)
               .Select(x => new FileTypeModel
               {
                   Id = x.Id,
                   Name = x.Name,
                   Identifier = (EFileTypeIdentifier)x.Identifier,
                   DefaultStorageLocation = x.DefaultStorageLocation,
                   FileExtensions = x.FileExtensions,
                   FileNamePattern = x.FileNamePattern,
                   MaxSize = x.MaxSize,
               })
               .FirstOrDefaultAsync();
            return dto;
        }
    }
}
