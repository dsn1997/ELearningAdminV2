using IIG.Core.Common.Enums.OrderBy;
using IIG.Core.Common.Models.Paging;

namespace IIG.Application.Models
{
    public class SearchingToeflChallengeContestMocktestRequest: BasePaginationRequest<EToeflChallengeContestMocktestOrderBy>
    {
        public Guid ContestId { get; set; }
        public Guid MocktestTypeId { get; set; }
    }
}
