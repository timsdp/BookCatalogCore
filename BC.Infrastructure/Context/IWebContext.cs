using BC.Infrastructure.DI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Infrastructure.Context
{
    public interface IWebContext
    {
        IRootContext RootContext { get; set; }
        IServiceProviderFactory Factory { get; }
    }
}
