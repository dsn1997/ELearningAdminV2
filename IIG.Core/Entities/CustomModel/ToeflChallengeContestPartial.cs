using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.Core.Entities
{
    public partial class ToeflChallengeContest
    {
        public virtual Province Province { get; set; }
        public virtual File IconFile { get; set; }
        public virtual AdminUser LastUpdateUser { get; set; }
        public virtual AdminUser CreatedUserUser { get; set; }

    }
}
