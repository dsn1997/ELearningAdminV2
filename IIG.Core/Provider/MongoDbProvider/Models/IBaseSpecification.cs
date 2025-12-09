using System.Linq.Expressions;

namespace IIG.Core.Providers.MongoDbProvider.Models
{
    public interface IBaseSpecification<T> where T : Document
    {
        Expression<Func<T, bool>> Criteria { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
        Expression<Func<T, object>> OrderBy { get; }
        bool IsDescending { get; }
        string Locale { get; set; }
    }
}
