using IIG.Application.Data;
using IIG.Core.Common.Models.Files;
using IIG.Core.Helpers;
using IIG.Core.Services;
using IIG.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IIG.Application.Services;
public class FileService : IFileService
{
    private readonly IFileDA _fileDA;
    private readonly IStorageService _storageService;
    private readonly IFileTypeDA _fileTypeDA;
    

    public FileService(IFileDA fileDa, IStorageService storageService,
         IFileTypeDA fileTypeDA)
    {
        _fileDA = fileDa;
        _storageService = storageService;
        
        _fileTypeDA = fileTypeDA;
    }

    public async Task InsertFileAsync(FileInsertModel model)
    {
        await _fileDA
            .InsertFileAsync(model);
    }

    public async Task UpdateFileAsync(FileInsertModel model)
    {
        await _fileDA
            .UpdateFileAsync(model);
    }

    public async Task<FileModel> GetByIdInternalAsync(Guid id)
    {
        return await _fileDA
             .GetByIdAsync(id);
    }

    public async Task DeleteFileAsync(Guid fileId)
    {
        await _fileDA
            .DeleteFileAsync(fileId);
    }

    public Task DeletePhysicalFileAsync(FileModel model)
    {
        _storageService.DeleteFile(model.FileName);
        _storageService.DeleteFile(model.ThumbnailStorageLocation);
        return Task.CompletedTask;
    }

    public async Task<FileModel> GetByIdAsync(Guid id)
    {
        return await GetByIdInternalAsync(id);
    }

    public async Task<FileDto> GetDtoByIdAsync(Guid id)
    {
        var file = await _fileDA
             .GetByIdAsync(id);

        if (file == null) return null;

        var result = new FileDto
        {
            Extension = file.Extension,
            FileName = file.FileName,
            DisplayName = file.DisplayName,
            FileTypeId = file.FileTypeId,
            Id = file.Id
        };

        return result;
    }

    public async Task<byte[]> GetFileContentByIdAsync(Guid id)
    {
        var file = await _fileDA
            .GetByIdAsync(id);

        if (file == null) return null;

        var contentType = FileHelper.GetMimeType(file.FileName);

        return new FileContentResult(await _storageService.GetBytes(file.FileName, file.IsPublic), contentType).FileContents;
    }

    public async Task<Dictionary<Guid, int>> GetFilesDuration(List<Guid> fileIds)
    {
        return await _fileDA.GetFilesDuration(fileIds);
    }
}
