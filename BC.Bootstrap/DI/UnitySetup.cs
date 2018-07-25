using BC.Infrastructure.DI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Bootstrap
{
    public class UnitySetup
    {
        static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            IUnityContainer container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        private static void RegisterTypes(IUnityContainer container)
        {
            UnityDependencyRegister.RegisterDependencyTypes(container);
        }

        public static IServiceProviderFactory CreateServiceProviderFactory()
        {
            return new ServiceProviderFactory(container.Value);
        }
    }
}
