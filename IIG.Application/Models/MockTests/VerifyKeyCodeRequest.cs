using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.Application.Models
{
    public class VerifyKeyCodeRequest
    {
        public Guid? MocktestTypeId { get; set; }
        public Guid? MocktestObjectId { get; set; }
        public string Keycode { get; set; }
        public string Browser { get; set; }
    }
}
