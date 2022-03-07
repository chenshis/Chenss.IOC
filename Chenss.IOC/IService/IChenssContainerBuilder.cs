using System;
using System.Collections.Generic;
using System.Text;

namespace Chenss.IOC.IService
{
    /// <summary>
    /// 容器（建议注册为单例）
    /// </summary>
    public interface IChenssContainerBuilder
    {
        #region 类型注册

        /// <summary>
        /// 瞬时注册
        /// </summary>
        /// <typeparam name="TService">抽象类</typeparam>
        /// <typeparam name="TImplementation">实现类</typeparam>
        void RegisterTransient<TService, TImplementation>() where TService : class where TImplementation : TService;
        /// <summary>
        /// 作用域
        /// </summary>
        /// <typeparam name="TService">抽象类</typeparam>
        /// <typeparam name="TImplementation">实现类</typeparam>
        void RegisterScoped<TService, TImplementation>() where TService : class where TImplementation : TService;
        /// <summary>
        /// 单例
        /// </summary>
        /// <typeparam name="TService">抽象类</typeparam>
        /// <typeparam name="TImplementation">实现类</typeparam>
        void RegisterSingleton<TService, TImplementation>() where TService : class where TImplementation : TService;

        #endregion

        #region 生成实例

        TService Resolve<TService>(); 

        #endregion
    }
}
