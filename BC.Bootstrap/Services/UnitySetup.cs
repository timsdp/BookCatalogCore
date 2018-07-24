using BC.Infrastructure.Interfaces.Base;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Bootstrap.Services
{
    public class UnitySetup
    {
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            IUnityContainer container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        public static void RegisterTypes(IUnityContainer container)
        {
            UnityDependencyResolver.RegisterTypes(container);
        }

        public static IUnityContainer GetUnityConfig()
        {
            return container.Value;
        }

        public static IServiceProviderFactory CreateFactory(IInternalRequestContext context)
        {
            return new ServiceProviderFactory(container.Value, context);
        }
    }
}
