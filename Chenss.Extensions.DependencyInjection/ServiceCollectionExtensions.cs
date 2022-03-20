using Chenss.IOC.IService;
using Microsoft.Extensions.DependencyInjection;
using Chenss.IOC.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chenss.IOC.Service;

namespace Chenss.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void Register(this IChenssContainerBuilder builder, IEnumerable<ServiceDescriptor> descriptors)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            builder.RegisterScoped<IChenssContainerBuilder, DefaultContainerBuilder>();
            builder.RegisterScoped<IServiceProvider, ChenssServiceProvider>();
            foreach (var descriptor in descriptors)
            {
                if (descriptor.ImplementationType != null)
                {
                    ConvertLifeTime(descriptor.Lifetime).AddRegisterType(descriptor.ServiceType, descriptor.ImplementationType);
                }
                else if (descriptor.ImplementationFactory != null)
                {
                    ConvertLifeTime(descriptor.Lifetime)
                        .ForDelegate(descriptor.ServiceType, (sp) => descriptor.ImplementationFactory(sp));
                }
                else
                {
                    ConvertLifeTime(descriptor.Lifetime).AddRegisterType(descriptor.ServiceType, descriptor.ImplementationType);
                }
            }
        }

        private static Chenss.IOC.TypeLifetime ConvertLifeTime(ServiceLifetime lifetime)
        {
            switch (lifetime)
            {
                case ServiceLifetime.Singleton:
                    return Chenss.IOC.TypeLifetime.Singleton;
                case ServiceLifetime.Scoped:
                    return Chenss.IOC.TypeLifetime.Scoped;
                case ServiceLifetime.Transient:
                    return Chenss.IOC.TypeLifetime.Transient;
                default:
                    throw new ArgumentNullException(nameof(lifetime));
            }
        }
    }
}
