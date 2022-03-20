using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Chenss.IOC
{
    public sealed class ServiceDescriptorContext
    {
        /// <summary>
        /// 实现类型
        /// </summary>
        public Type ImplementationType { get; set; }
        /// <summary>
        /// 接口类型
        /// </summary>
        public Type ServiceType { get; set; }
        /// <summary>
        /// 类型生命周期
        /// </summary>
        public TypeLifetime Lifetime { get; set; }
        /// <summary>
        ///  实例对象
        /// </summary>
        public object ImplementationInstance { get; set; }

        /// <summary>
        /// 实例工厂
        /// </summary>
        public Func<IServiceProvider, object> ImplementationFactory
        {
            get; set;
        }

        public override string ToString()
        {
            var lifetime = $"{nameof(ServiceType)}: {ServiceType} {nameof(Lifetime)}: {Lifetime} ";

            if (ImplementationType != null)
            {
                return lifetime + $"{nameof(ImplementationType)}: {ImplementationType}";
            }
            return lifetime + $"{nameof(ImplementationInstance)}: {ImplementationInstance}";
        }
    }
}
