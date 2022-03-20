using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chenss.IOC.Extensions
{
    public static class RegistrationExtensions
    {
        /// <summary>
        /// 服务描述上下文字典
        /// </summary>
        private static readonly ConcurrentDictionary<object, ServiceDescriptorContext> ServiceDescriptorCollection = new ConcurrentDictionary<object, ServiceDescriptorContext>();

        public static void AddRegisterType(this TypeLifetime lifetime, Type serviceType, Type implementationType)
        {
            ServiceDescriptorCollection.TryAdd(
                serviceType.FullName,
                new ServiceDescriptorContext
                {
                    ImplementationType = implementationType,
                    ServiceType = serviceType,
                    Lifetime = lifetime
                });
        }

        public static ServiceDescriptorContext GetDataContext(this Type serviceType)
        {
            ServiceDescriptorContext serviceDescriptor;
            if (serviceType == null)
            {
                throw new ArgumentException(nameof(serviceType));
            }
            var serviceKey = serviceType.FullName;
            if (serviceType.IsGenericType)
            {
                ServiceDescriptorCollection.TryGetValue(serviceKey, out serviceDescriptor);
                if (serviceDescriptor == null)
                {
                    serviceKey = serviceType.Namespace + "." + serviceType.Name;
                    ServiceDescriptorCollection.TryGetValue(serviceKey, out serviceDescriptor);
                    if (serviceDescriptor != null)
                    {
                        serviceDescriptor.ServiceType = serviceType;
                        serviceDescriptor.ImplementationType =
                            serviceDescriptor.ImplementationType.MakeGenericType(serviceType.GenericTypeArguments);
                        ServiceDescriptorCollection.TryAdd(serviceType.FullName, serviceDescriptor);
                    }
                    else
                    {
                        throw new ArgumentException(nameof(serviceDescriptor));
                    }
                }
            }
            if (ServiceDescriptorCollection.Count <= 0)
            {
                throw new Exception("DataContextCollection集合无数据");
            }
            ServiceDescriptorCollection.TryGetValue(serviceKey, out serviceDescriptor);
            return serviceDescriptor;
        }

        public static void ForDelegate(this TypeLifetime lifetime, Type serviceType, Func<IServiceProvider, object> func)
        {
            ServiceDescriptorCollection.TryAdd(
              serviceType.FullName,
              new ServiceDescriptorContext
              {
                  ServiceType = serviceType,
                  Lifetime = lifetime,
                  ImplementationFactory = func
              });
        }
    }
}
