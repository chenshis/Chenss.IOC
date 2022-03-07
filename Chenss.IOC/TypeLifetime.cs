using System;
using System.Collections.Generic;
using System.Text;

namespace Chenss.IOC
{
    public enum TypeLifetime
    {
        /// <summary>
        /// 单例
        /// </summary>
        Singleton,
        /// <summary>
        /// 作用域
        /// </summary>
        Scoped,
        /// <summary>
        /// 瞬时
        /// </summary>
        Transient
    }
}
