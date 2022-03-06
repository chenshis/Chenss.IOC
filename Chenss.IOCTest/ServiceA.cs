using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chenss.IOCTest
{
    public class ServiceA : IServiceA
    {
        public ServiceA(IServiceB serviceB)
        {
            //Console.WriteLine($"{nameof(ServiceA)}  {nameof(serviceB)}");
        }
    }

    public class ServiceB : IServiceB
    {
        public ServiceB()
        {
            //Console.WriteLine(nameof(ServiceB));
        }
    }
}
