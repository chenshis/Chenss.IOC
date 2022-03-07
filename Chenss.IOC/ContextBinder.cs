﻿using Chenss.IOC.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Chenss.IOC
{
    public class ContextBinder
    {
        private static readonly Func<ServiceDescriptorContext, Func<object>> FactoryBuilder = ContextBinderInvoker;
        private static readonly ConcurrentDictionary<ServiceDescriptorContext, Func<object>> FactoryCache =
            new ConcurrentDictionary<ServiceDescriptorContext, Func<object>>();
        private readonly Func<object> _factory;

        public ContextBinder(ServiceDescriptorContext context)
        {
            _factory = FactoryCache.GetOrAdd(context, FactoryBuilder);
        }
        public object Resolve() => _factory();

        public static Func<object> ContextBinderInvoker(ServiceDescriptorContext context)
        {
            return Expression.Lambda<Func<object>>(GetExpression(context), new ParameterExpression[0]).Compile();
        }

        private static Expression GetExpression(ServiceDescriptorContext context)
        {
            var ctor = context
                .ImplementationType
                .GetConstructors()
                .OrderByDescending(x => x.GetParameters().Length)
                .First();
            var parametersInfo = ctor.GetParameters();
            var argumentsExpression = new Expression[parametersInfo.Length];
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
