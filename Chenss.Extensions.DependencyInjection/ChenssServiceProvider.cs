using Chenss.IOC.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chenss.Extensions.DependencyInjection
{
    public class ChenssServiceProvider : IServiceProvider
    {
        private readonly IChenssContainerBuilder _builder;

        public ChenssServiceProvider(IChenssContainerBuilder builder)
        {
            this._builder = builder;
        }
        public object? GetService(Type serviceType)
        {
            return _builder.Resolve(serviceType);
        }
    }
}
