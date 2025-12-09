using IIG.Core.Common.MongoDataModels;
using IIG.Core.Entities;
using IIG.Core.Interface;
using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using IIG.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.Application.Services
{
    public interface ICommonService
    {
        Task<List<Tag>> GetAllTagsAsync();
    }
    public class CommonService : ICommonService
    {
        private readonly IRepository<Tag> _tagRepos;
        private readonly IMongoGenericRepository<MgLeftSectionModel> _mongoTagRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly ITagService _tagService;
        public CommonService(IRepository<Tag> tagRepos,
            IMongoGenericRepository<MgLeftSectionModel> mongoTagRepository,
            IUnitOfWorkManager unitOfWorkManager,
            ITagService tagService
            )
        {
            _tagRepos = tagRepos;
            _mongoTagRepository = mongoTagRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _tagService = tagService;
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            var id = await _tagService.InsertTag("TagTest1");
            var tags = await _tagRepos.FirstOrDefaultAsync(p=>p.Id == id);
            await using (var uow = _unitOfWorkManager.Begin())
            {
                var tag2 = await _tagRepos.FirstOrDefaultAsync(p => p.Id == id);

            }
            return new List<Tag>();
        }
    }
}
