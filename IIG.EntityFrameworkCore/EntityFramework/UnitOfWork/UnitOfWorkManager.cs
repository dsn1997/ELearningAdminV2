using IIG.Core.Interface;
using IIG.Core.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIG.EntityFrameworkCore.EntityFramework.UnitOfWork
{
    public class UnitOfWorkManager : IUnitOfWorkManager
    {
        private static readonly AsyncLocal<IUnitOfWork> _current = new();

        public IUnitOfWork Current => _current.Value;

        private readonly IServiceProvider _sp;
        private readonly IActiveTransactionProvider _txProvider;

        public UnitOfWorkManager(IServiceProvider sp, IActiveTransactionProvider txProvider)
        {
            _sp = sp;
            _txProvider = txProvider;
        }

        public IUnitOfWork Begin()
        {
            var outer = _current.Value;
            var uow = new UnitOfWork(this, _txProvider, outer);
            _current.Value = uow;
            return uow;
        }

        // internal: restore AsyncLocal to outer when UoW disposed
        internal void RestoreOuter(IUnitOfWork outer)
        {
            _current.Value = outer;
        }
    }
}
