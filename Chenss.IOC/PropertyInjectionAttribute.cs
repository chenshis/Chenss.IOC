using System;
using System.Collections.Generic;
using System.Text;

namespace Chenss.IOC
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PropertyInjectionAttribute : Attribute
    {
    }
}
