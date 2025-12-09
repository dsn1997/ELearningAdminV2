using IIG.Core.Common.ConfigureModels;

namespace IIG.Core.Providers.MongoDbProvider.Models
{
    public abstract class MgBaseListRequest<OrderableField>
    {
        public int? PageNum { get; set; } = Constants.Pagination.DefaultPage;
        public int? PageSize { get; set; } = Constants.Pagination.ItemsPerPage;
        public OrderableField SortColumn { get; set; }
        public bool Descending { get; set; } = false;
        public string Keyword { get; set; }
    }
}
