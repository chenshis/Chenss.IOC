using Chenss.IOC.Extensions;
using Chenss.IOC.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chenss.IOC.Service
{
    public class DefaultRegistrationBuilder : IRegistrationBuilder
    {
        public void RegisterTransient<TImplementation>() where TImplementation : class
        {
            Lifetime.Transient.AddRegisterType(typeof(TImplementation));
        }

        public void RegisterScoped<TImplementation>() where TImplementation : class
        {
            Lifetime.Scoped.AddRegisterType(typeof(TImplementation));
        }

        public void RegisterSingleton<TImplementation>() where TImplementation : class
        {
            Lifetime.Singleton.AddRegisterType(typeof(TImplementation));
        }

        public void RegisterTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : TService
        {
            Lifetime.Transient.AddRegisterType(typeof(TImplementation), typeof(TService));
        }

        public void RegisterScoped<TService, TImplementation>()
            where TService : class
            where TImplementation : TService
        {
            Lifetime.Scoped.AddRegisterType(typeof(TImplementation), typeof(TService));
        }

        public void RegisterSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : TService
        {
            Lifetime.Singleton.AddRegisterType(typeof(TImplementation), typeof(TService));
        }
    }
}
