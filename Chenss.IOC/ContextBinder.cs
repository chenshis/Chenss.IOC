using Chenss.IOC.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Chenss.IOC
{
    public class ContextBinder
    {
        private static readonly Func<ServiceDescriptorContext, Func<object>> FactoryBuilder = BinderInvoker;
        private static readonly ConcurrentDictionary<ServiceDescriptorContext, Func<object>> FactoryCache =
            new ConcurrentDictionary<ServiceDescriptorContext, Func<object>>();
        private readonly Func<object> _factory;

        public ContextBinder(ServiceDescriptorContext context)
        {
            _factory = FactoryCache.GetOrAdd(context, FactoryBuilder);
        }
        public object Resolve() => _factory();

        /// <summary>
        /// 绑定执行
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static Func<object> BinderInvoker(ServiceDescriptorContext context)
        {
            List<Expression> expressions = new List<Expression>();
            var variableExpression = Expression.Variable(context.ImplementationType);
            var newExpression = GetExpressionByContext(context);
            var newObject = Expression.Assign(variableExpression, newExpression);
            expressions.Add(newObject);

            IEnumerable<PropertyInfo> properties = GetPropertiesByContext(context);
            foreach (var property in properties)
            {
                var propContext = property.PropertyType.FullName.GetDataContext();
                var propExpression = Expression.Property(variableExpression, property);
                var propNewExpression = GetExpressionByContext(propContext);
                var propAssignExpression = Expression.Assign(propExpression, propNewExpression);
                expressions.Add(propAssignExpression);
            }
            var blockExpression = Expression.Block(new[] { variableExpression }, Expression.Block(expressions), variableExpression);

            return Expression.Lambda<Func<object>>(blockExpression, new ParameterExpression[0]).Compile();
        }

        /// <summary>
        ///  new expression
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static Expression GetExpressionByContext(ServiceDescriptorContext context)
        {
            var ctor = GetConstructorByContext(context);
            var parametersInfo = ctor.GetParameters();
            var argumentsExpression = new Expression[parametersInfo.Length];
            for (int i = 0; i < parametersInfo.Length; i++)
            {
                var parameterType = parametersInfo[i].ParameterType;
                var dataContext = parameterType.FullName.GetDataContext();
                if (dataContext != null)
                {
                    argumentsExpression[i] = GetExpressionByContext(dataContext);
                }
            }
            var newExpression = Expression.New(ctor, argumentsExpression);
            return newExpression;
        }

        /// <summary>
        /// 获取构造函数
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static ConstructorInfo GetConstructorByContext(ServiceDescriptorContext context)
        {
            var ctors = context.ImplementationType.GetConstructors();
            var ctor = ctors
                .Where(c => c.IsDefined(typeof(ConstructorInjectionAttribute), true))
                .OrderByDescending(c => c.GetParameters().Length)
                .FirstOrDefault();
            if (ctor == null)
            {
                ctor = ctors.OrderByDescending(x => x.GetParameters().Length).First();
            }

            return ctor;
        }

        /// <summary>
        /// 根据特性获取属性
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static IEnumerable<PropertyInfo> GetPropertiesByContext(ServiceDescriptorContext context)
        {
            return context
                .ImplementationType
                .GetProperties()
                .Where(p => p.IsDefined(typeof(PropertyInjectionAttribute), true));
        }
    }
}
