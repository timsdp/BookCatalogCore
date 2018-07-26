using BC.Bootstrap.Context;
using BC.Business.Author;
using BC.Business.Book;
using BC.Business.Context;
using BC.Data.Repositories;
using BC.Infrastructure.Context;
using BC.Infrastructure.Interfaces.Repository;
using BC.Infrastructure.Interfaces.Service;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Bootstrap
{
    public class UnityDependencyRegister
    {
        private static IUnityContainer _container;

        public static void RegisterDependencyTypes(IUnityContainer container)
        {
            _container = container;

            RegisterContexts();
            RegisterBusinessTypes();
            RegisterDataTypes();
        }

        private static void RegisterContexts()
        {
            _container.RegisterType<IRequestContext, RequestContext>();
            _container.RegisterType<IBusinessContext, BusinessContext>();
        }

        private static void RegisterBusinessTypes()
        {
            _container.RegisterType<IAuthorService, AuthorService>();
            _container.RegisterType<IBookService, BookService>();
        }

        private static void RegisterDataTypes()
        {
            _container.RegisterType<IBookRepository, BookRepository>();
            _container.RegisterType<IAuthorRepository, AuthorRepository>();
        }
    }
}
