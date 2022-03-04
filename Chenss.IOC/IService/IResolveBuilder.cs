﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Chenss.IOC.IService
{
    public interface IResolveBuilder
    {
        TService Resolve<TService>();
    }
}
