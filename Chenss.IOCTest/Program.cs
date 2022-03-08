using Chenss.IOC.IService;
using Chenss.IOC.Service;
using System;

namespace Chenss.IOCTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IChenssContainerBuilder builder = new DefaultContainerBuilder();
            builder.RegisterTransient<IServiceA, ServiceA>();
            builder.RegisterTransient<IServiceB, ServiceB>();
            builder.RegisterTransient<IServiceC, ServiceC>();
            builder.RegisterTransient<IServiceD, ServiceD>();




            var a = builder.Resolve<IServiceA>();
            var a1 = builder.Resolve<IServiceA>();
            var b = builder.Resolve<IServiceB>();
            var c = builder.Resolve<IServiceC>();
            var d = builder.Resolve<IServiceD>();


            var container = builder.CreateScope();
            var a2 = container.Resolve<IServiceA>();

            Console.WriteLine(Object.ReferenceEquals(a, a1));
            Console.WriteLine(Object.ReferenceEquals(a, a2));
            //var b = builder.Resolve<IServiceB>();
            //var c = builder.Resolve<IServiceC>();
            //var d = builder.Resolve<IServiceD>();
            Console.WriteLine("Hello World!");
        }
    }
}
