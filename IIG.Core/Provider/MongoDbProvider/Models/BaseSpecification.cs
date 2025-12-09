using IIG.Core.Common.ConfigureModels;
using System.Linq.Expressions;

namespace IIG.Core.Providers.MongoDbProvider.Models
{
    public class BaseSpecification<T> : IBaseSpecification<T> where T : Document
    {
        protected BaseSpecification()
        {
        }

        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; private set; }
        public Expression<Func<T, object>> OrderBy { get; private set; }

        public int Take { get; private set; } = Constants.Pagination.ItemsPerPage;
        public int Skip { get; private set; } = 0;
        public bool IsPagingEnabled { get; private set; } = false;
        public bool IsDescending { get; private set; } = false;
        public string Locale { get; set; } = "en";

        public void SkipPaging()
        {
            IsPagingEnabled = false;
        }

        protected virtual void ApplyOrder(Expression<Func<T, object>> orderByExpression, bool isDescending = false)
        {
            OrderBy = orderByExpression;
            IsDescending = isDescending;
        }

        protected virtual void AddCriteria(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        protected virtual void ApplyPaging(int? page, int? pageSize)
        {
            pageSize = pageSize == null || pageSize <= 0 ? Constants.Pagination.ItemsPerPage : pageSize;
            page = page == null || page <= 0 ? Constants.Pagination.DefaultPage : page;

            Skip = (page.Value - 1) * pageSize.Value;
            Take = pageSize.Value;
            IsPagingEnabled = true;
        }
    }
}
