using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Infrastructure.DI
{
    public interface IServiceProviderFactory : IServiceProvider
    {
        object GetService(Type serviceType, params object[] resolveParams);
        ServiceType GetService<ServiceType>();
        TService GetService<TService>(params object[] resolveParams);
        TService GetService<TService>(string name, params object[] resolveParams);
    }
}
