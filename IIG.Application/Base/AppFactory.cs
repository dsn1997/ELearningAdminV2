using AutoMapper;
using EntityFrameWorkCore;
using IIG.Core.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace IIG.Core.Base
{
    public interface IAppFactory
    {
        IAppSession AppSession { get; }
        IUnitOfWorkManager UnitOfWorkManager { get; }
        IMapper Mapper { get; }
    }

    public class AppFactory : IAppFactory
    {
        #region LazyGetRequiredService
        protected IServiceProvider ServiceProvider { get; set; }
        protected readonly object ServiceProviderLock = new object();
        private readonly IServiceScopeFactory _scopeFactory;

        public AppFactory(IServiceProvider serviceProvider, IServiceScopeFactory scopeFactory)
        {
            ServiceProvider = serviceProvider;
            _scopeFactory = scopeFactory;
        }

        protected TService LazyGetRequiredService<TService>(ref TService reference)
          => LazyGetRequiredService(typeof(TService), ref reference);
        protected TRef LazyGetRequiredService<TRef>(Type serviceType, ref TRef reference)
        {
            if (reference != null) return reference;
            lock (ServiceProviderLock)
            {
                if (reference == null)
                {
                    reference = (TRef)ServiceProvider.GetRequiredService(serviceType);
                }
            }
            return reference;
        }
        #endregion

        private IAppSession _appSession;
        public IAppSession AppSession { get => LazyGetRequiredService(ref _appSession); }

        private IUnitOfWorkManager? _unitOfWorkManager;
        public IUnitOfWorkManager UnitOfWorkManager
        {
            get => LazyGetRequiredService(ref _unitOfWorkManager);
        }

        private IMapper _mapper;
        public IMapper Mapper { get => LazyGetRequiredService(ref _mapper); }
    }
}
