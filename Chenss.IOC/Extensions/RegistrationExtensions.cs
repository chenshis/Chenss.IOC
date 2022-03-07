using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Chenss.IOC.Extensions
{
    public static class RegistrationExtensions
    {
        /// <summary>
        /// 服务描述上下文字典
        /// </summary>
        private static readonly ConcurrentDictionary<object, ServiceDescriptorContext> ServiceDescriptorCollection = new ConcurrentDictionary<object, ServiceDescriptorContext>();

        public static void AddRegisterType(this TypeLifetime lifetime, Type implementationType, Type serviceType)
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

        public static ServiceDescriptorContext GetDataContext(this object key)
        {
            if (ServiceDescriptorCollection.Count <= 0)
            {
                throw new Exception("DataContextCollection集合无数据");
            }
            ServiceDescriptorCollection.TryGetValue(key, out var dataContext);
            return dataContext;
        }
    }
}
