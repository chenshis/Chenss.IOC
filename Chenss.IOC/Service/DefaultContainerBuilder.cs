using Chenss.IOC.Extensions;
using Chenss.IOC.IService;

namespace Chenss.IOC.Service
{
    public class DefaultContainerBuilder : IChenssContainerBuilder
    {
        public void RegisterTransient<TService, TImplementation>()
           where TService : class
           where TImplementation : TService
        {
            TypeLifetime.Transient.AddRegisterType(typeof(TImplementation), typeof(TService));
        }

        public void RegisterScoped<TService, TImplementation>()
            where TService : class
            where TImplementation : TService
        {
            TypeLifetime.Scoped.AddRegisterType(typeof(TImplementation), typeof(TService));
        }

        public void RegisterSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : TService
        {
            TypeLifetime.Singleton.AddRegisterType(typeof(TImplementation), typeof(TService));
        }

        public TService Resolve<TService>()
        {
            var context = typeof(TService).FullName.GetDataContext();
            // 单例校验结果
            if (context.Lifetime == TypeLifetime.Singleton && context.ImplementationInstance != null)
            {
                return (TService)context.ImplementationInstance;
            }
            var constructorBinder = new ContextBinder(context);
            var implementationInstance = constructorBinder.Resolve();
            // 单例实体对象赋值
            if (context.Lifetime == TypeLifetime.Singleton) 
                context.ImplementationInstance = implementationInstance;

            return (TService)implementationInstance;
        }
    }
}
