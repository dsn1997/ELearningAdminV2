using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.Core.Entities
{
    public partial class ToeflChallengeRankingScore
    {
        public virtual ICollection<ToeflChallengeRankingScoreMocktestType> RankingScoreMocktests { get; set; } = new List<ToeflChallengeRankingScoreMocktestType>();


    }
}
