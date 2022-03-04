using System;
using System.Collections.Generic;
using System.Text;

namespace Chenss.IOC.IService
{
    public interface IRegistrationBuilder
    {
        /// <summary>
        /// 瞬时注册
        /// </summary>
        /// <typeparam name="TImplementation">实现类</typeparam>
        void RegisterTransient<TImplementation>() where TImplementation : class;
        /// <summary>
        /// 瞬时注册
        /// </summary>
        /// <typeparam name="TService">抽象类</typeparam>
        /// <typeparam name="TImplementation">实现类</typeparam>
        void RegisterTransient<TService, TImplementation>() where TService : class where TImplementation : TService;
        /// <summary>
        /// 作用域
        /// </summary>
        /// <typeparam name="TImplementation">实现类</typeparam>
        void RegisterScoped<TImplementation>() where TImplementation : class;
        /// <summary>
        /// 作用域
        /// </summary>
        /// <typeparam name="TService">抽象类</typeparam>
        /// <typeparam name="TImplementation">实现类</typeparam>
        void RegisterScoped<TService, TImplementation>() where TService : class where TImplementation : TService;
        /// <summary>
        /// 单例
        /// </summary>
        /// <typeparam name="TImplementation">实现类</typeparam>
        void RegisterSingleton<TImplementation>() where TImplementation : class;
        /// <summary>
        /// 单例
        /// </summary>
        /// <typeparam name="TService">抽象类</typeparam>
        /// <typeparam name="TImplementation">实现类</typeparam>
        void RegisterSingleton<TService, TImplementation>() where TService : class where TImplementation : TService;
    }
}
