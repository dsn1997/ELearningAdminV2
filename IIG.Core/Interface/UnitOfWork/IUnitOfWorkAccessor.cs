using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.Core.Interface.UnitOfWork
{
    public interface IUnitOfWorkAccessor
    {
        IUnitOfWork Current { get; set; }
    }
}
