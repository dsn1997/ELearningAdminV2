using IIG.Core.Common.ConfigureModels;
using IIG.Core.Common.MongoDataModels.Keycodes;
using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using IIG.Core.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace IIG.Core.Services;
public class MongoMockTestKeyCodeService :  IMongoMockTestKeyCodeService
{
    public MongoMockTestKeyCodeService(IOptions<DbConnectionStringsOptions> mongoDbSettings) 
    {
    }
}
