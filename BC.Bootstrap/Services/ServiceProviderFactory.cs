using BC.Infrastructure.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Bootstrap.Services
{
    public class ServiceProviderFactory : IServiceProviderFactory
    {
        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }
    }
}
