﻿using Chenss.IOC.IService;
using Chenss.IOC.Service;
using System;

namespace Chenss.IOCTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IRegistrationBuilder builder = new DefaultRegistrationBuilder();
            builder.RegisterTransient<IServiceA, ServiceA>();
            IResolveBuilder resolve = new DefaultResolveBuilder();
            resolve.Resolve<IServiceA>();
            Console.WriteLine("Hello World!");
        }
    }
}