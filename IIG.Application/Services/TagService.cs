using IIG.Core.Common.MongoDataModels;
using IIG.Core.Entities;
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
    public interface ITagService
    {
        Task<Guid> InsertTag(string name);
    }
    public class TagService : ITagService
    {
        private readonly IRepository<Tag> _tagRepos;
        public TagService(IRepository<Tag> tagRepos, IMongoGenericRepository<MgLeftSectionModel> mongoTagRepository)
        {
            _tagRepos = tagRepos;
        }

        public async Task<Guid> InsertTag(string name)
        {
            var entity = new Tag
            {
                Id = Guid.NewGuid(),
                Name = $"{name}",
                Type = Core.Common.Enums.ETagType.Question,
            };
            await _tagRepos.InsertAsync(entity);
            return entity.Id;
        }
    }
}
