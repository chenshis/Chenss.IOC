using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Chenss.IOC
{
    internal class DataContext
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
        public Lifetime TypeLifetime { get; set; }
        /// <summary>
        ///  单例实例
        /// </summary>
        public object SingletonInstance { get; set; }
    }
}
