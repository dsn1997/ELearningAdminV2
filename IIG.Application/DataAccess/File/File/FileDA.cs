using IIG.Core.Base;
using IIG.Core.Common.Models.Files;
using IIG.Core.Entities;
using IIG.Core.Helper;
using IIG.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace IIG.Application.Data
{
    public class FileDA : IFileDA
    {
        private readonly IAppFactory _appFactory;
        private readonly IRepository<Core.Entities.File> _fileRepos;

        public FileDA(IAppFactory appFactory,
                     IRepository<Core.Entities.File> fileRepos)
        {
            _appFactory = appFactory;
            _fileRepos = fileRepos;
        }

        public async Task InsertFileAsync(FileInsertModel model)
        {
            var entity = new Core.Entities.File()
            {
                Id = model.Id,
                FileName = model.FileName,
                Extension = model.Extension,
                StorageLocation = model.StorageLocation,
                DisplayName = model.DisplayName,
                FileTypeId = model.FileTypeId,
                CourseId = model.CourseId,
                UserId = model.UserId,
                CourseTeacherId = model.CourseTeacherId,
                ThumbnailStorageLocation = model.ThumbnailStorageLocation,
                SmallStorageLocation = model.SmallStorageLocation,
                MediumStorageLocation = model.MediumStorageLocation,
                LargeStorageLocation = model.LargeStorageLocation,
                ImageOrder = model.ImageOrder,
                FileSize = model.FileSize,
                IsPublic = model.IsPublic,
                IsMigrate = model.IsMigrate,
                IsActive = model.IsActive,
                AudioDuration = model.AudioDuration,
                NewsId = model.NewsId
            };
            await _fileRepos.InsertAsync(entity);
        }

        public async Task UpdateFileAsync(FileInsertModel model)
        {
            var entity = await _fileRepos.GetByIdAsync(model.Id);
            if (entity == null)
            {
                throw new Exception("File not found");
            }
            entity.FileName = model.FileName;
            entity.Extension = model.Extension;
            entity.StorageLocation = model.StorageLocation;
            entity.DisplayName = model.DisplayName;
            entity.FileTypeId = model.FileTypeId;
            entity.CourseId = model.CourseId;
            entity.UserId = model.UserId;
            entity.CourseTeacherId = model.CourseTeacherId;
            entity.ThumbnailStorageLocation = model.ThumbnailStorageLocation;
            entity.SmallStorageLocation = model.SmallStorageLocation;
            entity.MediumStorageLocation = model.MediumStorageLocation;
            entity.LargeStorageLocation = model.LargeStorageLocation;
            entity.ImageOrder = model.ImageOrder;
            entity.FileSize = model.FileSize;
            entity.IsPublic = model.IsPublic;
            entity.IsMigrate = model.IsMigrate;
            entity.IsActive = model.IsActive;
            entity.AudioDuration = model.AudioDuration;
            entity.NewsId = model.NewsId;

            await _fileRepos.UpdateAsync(entity);
        }

        public async Task DeleteFileAsync(Guid fileId)
        {
            await _fileRepos.DeleteAsync(fileId);
        }

        public async Task<FileModel> GetByIdAsync(Guid id)
        {
            var entity = await _fileRepos.GetByIdAsync(id);
            if (entity == null)
            {
                return null;
            }
            var dto = _appFactory.Mapper.Map<FileModel>(entity);
            return dto;
        }

        public async Task<Dictionary<Guid, int>> GetFilesDuration(List<Guid> fileIds)
        {
            var files = await _fileRepos.GetAll().Where(x => fileIds.Contains(x.Id))
                                                         .Select(x => new
                                                         {
                                                             x.Id,
                                                             x.AudioDuration
                                                         }).ToListAsync();

            var dict = files.ToDictionary(x => x.Id, x => x.AudioDuration.HasValue ? (int)x.AudioDuration : 0);
            return dict;
        }
    }
}