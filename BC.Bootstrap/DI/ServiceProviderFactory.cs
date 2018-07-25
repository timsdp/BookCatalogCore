using BC.Infrastructure.DI;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Text;


namespace BC.Bootstrap
{
    public class ServiceProviderFactory : IServiceProviderFactory
    {
        #region Constructors
        public ServiceProviderFactory(IUnityContainer container)
        {
            _container = container;
        }
        #endregion

        IUnityContainer _container;

        public object GetService(Type serviceType, params object[] resolveParams)
        {
            return _container.Resolve(serviceType, new ServiceProviderParameterResolver(resolveParams));
        }

        public object GetService(Type serviceType)
        {
            return _container.Resolve(serviceType);
        }

        public ServiceType GetService<ServiceType>()
        {
            return (ServiceType)this.GetService(typeof(ServiceType));
        }

        public TService GetService<TService>(params object[] resolveParams)
        {
            return _container.Resolve<TService>(new ServiceProviderParameterResolver(resolveParams));
        }

        public TService GetService<TService>(string name, params object[] resolveParams)
        {
            return _container.Resolve<TService>(name, new ServiceProviderParameterResolver(resolveParams));
        }
    }
}
