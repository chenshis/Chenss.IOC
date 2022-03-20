using Chenss.IOC.IService;
using Chenss.IOC.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chenss.Extensions.DependencyInjection
{
    public class ChenssServiceProviderFactory : IServiceProviderFactory<IChenssContainerBuilder>
    {
        public IChenssContainerBuilder CreateBuilder(IServiceCollection services)
        {
            IChenssContainerBuilder builder = new DefaultContainerBuilder();
            builder.Register(services);
            return builder;
        }

        public IServiceProvider CreateServiceProvider(IChenssContainerBuilder containerBuilder)
        {
            return new ChenssServiceProvider(containerBuilder);
        }
    }
}
