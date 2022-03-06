using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Chenss.IOC.Extensions
{
    internal static class TypeExtensions
    {
        /// <summary>
        /// 数据上下文字典
        /// </summary>
        private static readonly ConcurrentDictionary<object, DataContext> DataContextCollection = new ConcurrentDictionary<object, DataContext>();

        /// <summary>
        /// 缓存对象
        /// </summary>
        private static Func<object> _funcObjBuilder;

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

        public static object ResolveBuilder(this DataContext context)
        {
            if (_funcObjBuilder == null)
            {
                var lambda = Expression.Lambda<Func<object>>(GetExpression(context), new ParameterExpression[0]);
                _funcObjBuilder = lambda.Compile();
            }
            return _funcObjBuilder.Invoke();
        }

        private static Expression GetExpression(DataContext context)
        {
            var ctor = context
                .ImplementationType
                .GetConstructors()
                .OrderByDescending(x => x.GetParameters().Length)
                .First();
            var parametersInfo = ctor.GetParameters();
            var argumentsExpression = new Expression[parametersInfo.Length];
            var parameterExpression = Expression.Parameter(typeof(object[]), "arg");
            for (int i = 0; i < parametersInfo.Length; i++)
            {
                var parameterType = parametersInfo[i].ParameterType;
                var dataContext = parameterType.FullName.GetDataContext();
                if (dataContext != null)
                {
                    argumentsExpression[i] = GetExpression(dataContext);
                }
            }
            var newExpression = Expression.New(ctor, argumentsExpression);
            return newExpression;
        }
    }
}
