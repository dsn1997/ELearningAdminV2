using IIG.Core.Common.Enums;
using IIG.Core.Providers.MongoDbProvider.Models;
using LinqKit;

namespace IIG.Core.Common.MongoDataModels.MockTests
{
    public class MockTestFilterSpecification : BaseSpecification<MgMockTestModel>
    {
        public MockTestFilterSpecification(MgMockTestListRequest request, bool paged = true)
        {
            if (paged)
            {
                ApplyPaging(request.PageNum, request.PageSize);
            }

            switch (request.SortColumn)
            {
                case EMockTestOrderableField.Name:
                    ApplyOrder(p => p.GeneralInfo.Names[0].Name, request.Descending);
                    break;
                case EMockTestOrderableField.Status:
                    ApplyOrder(p => p.GeneralInfo.Status, request.Descending);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            var predicate = PredicateBuilder.New<MgMockTestModel>(true);

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                predicate = predicate.And(c => c.GeneralInfo.Names.Any(x => x.Name.ToLower().Contains(request.Keyword.ToLower())));
            }

            if (request.Ids != null && request.Ids.Any())
            {
                predicate = predicate.And(c => request.Ids.Contains(c.MockTestId));
            }

            if (request.MockTestTypeId.HasValue)
            {
                predicate = predicate.And(c => c.GeneralInfo.MockTestType.Id == request.MockTestTypeId);
            }

            if (request.MockTestObjectId.HasValue)
            {
                predicate = predicate.And(c => c.GeneralInfo.MockTestObject.Id == request.MockTestObjectId);
            }

            if (request.Status.HasValue)
            {
                predicate = predicate.And(c => c.GeneralInfo.Status == request.Status);
            }

            Locale = "vi";

            AddCriteria(predicate);
        }
    }
}
