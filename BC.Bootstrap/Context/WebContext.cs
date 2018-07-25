using BC.Infrastructure.Context;
using BC.Infrastructure.DI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Bootstrap.Context
{
    public class WebContext : IWebContext
    {
        public IRootContext RootContext { get; set; }

        public IServiceProviderFactory Factory => RootContext.Factory;

        public WebContext(string connectionString)
        {
            RootContext = new RootContext(connectionString);
        }
    }
}
