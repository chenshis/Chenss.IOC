using Chenss.IOC.Extensions;
using Chenss.IOC.IService;
using System;
using System.Collections.Concurrent;

namespace Chenss.IOC.Service
{
    public class DefaultContainerBuilder : IChenssContainerBuilder
    {
        /// <summary>
        /// 作用域字典
        /// </summary>
        private ConcurrentDictionary<object, ServiceDescriptorContext> ServiceScopeCollection = new ConcurrentDictionary<object, ServiceDescriptorContext>();

        public void RegisterTransient<TService, TImplementation>()
           where TService : class
           where TImplementation : TService
        {
            RegisterTransient(typeof(TService), typeof(TImplementation));
        }

        public void RegisterTransient(System.Type serviceType, System.Type serviceImplementation)
        {
            TypeLifetime.Transient.AddRegisterType(serviceType, serviceImplementation);
        }

        public void RegisterScoped<TService, TImplementation>()
            where TService : class
            where TImplementation : TService
        {
            RegisterScoped(typeof(TService), typeof(TImplementation));
        }

        public void RegisterScoped(System.Type serviceType, System.Type serviceImplementation)
        {
            TypeLifetime.Scoped.AddRegisterType(serviceType, serviceImplementation);
        }

        public void RegisterSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : TService
        {
            RegisterSingleton(typeof(TService), typeof(TImplementation));
        }

        public void RegisterSingleton(System.Type serviceType, System.Type serviceImplementation)
        {
            TypeLifetime.Singleton.AddRegisterType(serviceType, serviceImplementation);
        }

        public IChenssContainerBuilder CreateScope()
        {
            // 实例化作用域容器
            ServiceScopeCollection = new ConcurrentDictionary<object, ServiceDescriptorContext>();
            return this;
        }

        public TService Resolve<TService>() => (TService)Resolve(typeof(TService));

        public object Resolve(Type serviceType)
        {
            var context = serviceType.GetDataContext();
            if (ValidateImplInstance(context))
            {
                return context.ImplementationInstance;
            }
            if (context.ImplementationFactory != null)
            {
                context.ImplementationFactory(Resolve<System.IServiceProvider>());
            }
            var constructorBinder = new ContextBinder(context);
            var implementationInstance = constructorBinder.Resolve();
            SetImplInstance(context, implementationInstance);
            return implementationInstance;
        }

        #region 私有方法

        private bool ValidateImplInstance(ServiceDescriptorContext context)
        {
            if (context.Lifetime == TypeLifetime.Singleton && context.ImplementationInstance != null)
            {
                return true;
            }
            if (context.Lifetime == TypeLifetime.Scoped)
            {
                if (ServiceScopeCollection.TryGetValue(context.ServiceType.FullName, out var scope))
                {
                    if (scope.ImplementationInstance != null)
                    {
                        return true;
                    }
                }
                context.ImplementationInstance = null;
            }
            return false;
        }

        private void SetImplInstance(ServiceDescriptorContext context, object implementationInstance)
        {
            if (context.Lifetime == TypeLifetime.Singleton)
            {
                context.ImplementationInstance = implementationInstance;
            }
            else if (context.Lifetime == TypeLifetime.Scoped)
            {
                context.ImplementationInstance = implementationInstance;
                ServiceScopeCollection.GetOrAdd(context.ServiceType.FullName, context);
            }
        }
        #endregion
    }
}
