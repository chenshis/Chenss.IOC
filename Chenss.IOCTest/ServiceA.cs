using Chenss.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chenss.IOCTest
{
    public class ServiceA<T> : IServiceA<T>
    {
        [PropertyInjection]
        public IServiceC serviceC { get; set; }

        public ServiceA(IServiceB serviceB)
        {
            Console.WriteLine($"{nameof(ServiceA<T>)}");
        }
    }

    public class ServiceB : IServiceB
    {
        public ServiceB(IServiceC serviceC, IServiceD serviceD)
        {
            //Console.WriteLine(nameof(ServiceB));
        }
    }
    public class ServiceC : IServiceC
    {
        public ServiceC(IServiceD serviceD)
        {
            //Console.WriteLine(nameof(ServiceC));
        }
    }
    public class ServiceD : IServiceD
    {
        public ServiceD()
        {
            //Console.WriteLine(nameof(ServiceD));
        }
    }
}
