using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.Core.Providers.BackgroudJob
{
    public interface IRabbitListener
    {
        Task RegisterAsync();
    }
}
