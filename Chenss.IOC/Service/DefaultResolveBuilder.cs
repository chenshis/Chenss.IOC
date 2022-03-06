using Chenss.IOC.Extensions;
using Chenss.IOC.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Chenss.IOC.Service
{
    public class DefaultResolveBuilder : IResolveBuilder
    {
        public TService Resolve<TService>()
        {
            var context = typeof(TService).FullName.GetDataContext();
            return (TService)context.ResolveBuilder();
        }

        private object GetResolveObject(DataContext context)
        {
            var objList = new List<object>();
            var ctor = context.ImplementationType.GetConstructors().OrderByDescending(x => x.GetParameters().Length).First();
            foreach (var parameter in ctor.GetParameters())
            {
                var parameterType = parameter.ParameterType;
                var dataContext = parameterType.FullName.GetDataContext();
                if (dataContext != null)
                {
                    var obj = GetResolveObject(dataContext);
                    objList.Add(obj);
                }
            }
            return Activator.CreateInstance(context.ImplementationType, objList.ToArray());
        }

        private object GetObject(DataContext context, object instance)
        {
            switch (context.TypeLifetime)
            {
                case Lifetime.Singleton:
                    if (instance == null)
                    {
                        if (context.SingletonInstance != null)
                        {
                            return context.SingletonInstance;
                        }
                    }
                    else
                    {
                        context.SingletonInstance = instance;
                    }
                    break;
                case Lifetime.Scoped:
                    break;
                case Lifetime.Transient:
                    break;
                default:
                    break;
            }
            return null;
        }
    }
}
