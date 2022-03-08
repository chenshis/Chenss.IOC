using System;
using System.Collections.Generic;
using System.Text;

namespace Chenss.IOC
{
    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false)]
    public class ConstructorInjectionAttribute : Attribute
    {

    }
}
