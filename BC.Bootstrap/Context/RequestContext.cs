using BC.Infrastructure.Context;
using BC.Infrastructure.DI;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Bootstrap.Context
{
    public class RequestContext : IRequestContext
    {
        public IRootContext RootContext { get; set; }

        public IServiceProviderFactory Factory => RootContext.Factory;

        public RequestContext(string connectionString)
        {
            RootContext = new RootContext(connectionString);
        }
    }
}
