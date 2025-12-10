using AutoMapper;
using EntityFrameWorkCore;
using IIG.Core.Interface;
using IIG.Core.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace IIG.Core.Base
{
    public interface IAppFactory
    {
        IAppSession AppSession { get; }
        IUnitOfWorkManager UnitOfWorkManager { get; }
        IMapper Mapper { get; }
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;

    }

    public class AppFactory : IAppFactory
    {
        #region LazyGetRequiredService
        protected IServiceProvider ServiceProvider { get; set; }
        protected readonly object ServiceProviderLock = new object();
        private readonly IServiceScope _scopeIfCreated;

        public AppFactory(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
        {
            ServiceProvider = serviceProvider;
            var requestProvider = httpContextAccessor?.HttpContext?.RequestServices;

            if (requestProvider != null)
            {
                ServiceProvider = requestProvider;
            }
            else
            {
                // 🔴 Không có HTTP scope → tạo scope mới
                _scopeIfCreated = serviceProvider.CreateScope();
                ServiceProvider = _scopeIfCreated.ServiceProvider;
            }
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

        #region Repository

        private Dictionary<Type, object> _repositories;
        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            _repositories ??= new Dictionary<Type, object>();

            var type = typeof(TEntity);
            lock (ServiceProviderLock)
            {
                if (!_repositories.ContainsKey(type))
                {
                    _repositories[type] = ServiceProvider.GetRequiredService<IRepository<TEntity>>();
                }
            }

            return (IRepository<TEntity>)_repositories[type];
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
