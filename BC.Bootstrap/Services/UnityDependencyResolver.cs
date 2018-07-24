using BC.Data.Context;
using BC.Infrastructure.Interfaces;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Bootstrap.Services
{
    public class UnityDependencyResolver
    {
        protected static IUnityContainer _container;

        public static void RegisterTypes(IUnityContainer container)
        {
            container = _container;

            container.RegisterType<IDataContext, DapperContext>();

        }
    }
    }
