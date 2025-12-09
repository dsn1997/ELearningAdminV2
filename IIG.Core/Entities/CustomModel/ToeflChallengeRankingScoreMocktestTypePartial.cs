using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.Core.Entities
{
    public partial class ToeflChallengeRankingScoreMocktestType
    {
        
        public MocktestType MocktestType { get; set; }
        public ToeflChallengeRankingScore RankingScore { get; set; }
     
    }
}
