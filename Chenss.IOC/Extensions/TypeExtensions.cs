using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Chenss.IOC.Extensions
{
    internal static class TypeExtensions
    {
        /// <summary>
        /// 数据上下文字典
        /// </summary>
        private static readonly ConcurrentDictionary<object, DataContext> DataContextCollection = new ConcurrentDictionary<object, DataContext>();


        public static void AddRegisterType(this Lifetime lifetime, Type implementationType)
        {
            DataContextCollection.TryAdd(implementationType.FullName, new DataContext { ImplementationType = implementationType, TypeLifetime = lifetime });
        }

        public static void AddRegisterType(this Lifetime lifetime, Type implementationType, Type serviceType)
        {
            DataContextCollection.TryAdd(
                serviceType.FullName,
                new DataContext
                {
                    ImplementationType = implementationType,
                    ServiceType = serviceType,
                    TypeLifetime = lifetime
                });
        }

        public static DataContext GetDataContext(this object key)
        {
            if (DataContextCollection.Count <= 0)
            {
                throw new Exception("DataContextCollection集合无数据");
            }
            DataContextCollection.TryGetValue(key, out var dataContext);
            return dataContext;
        }
    }
}
