using AutoMapper;
using BC.Bootstrap;
using BC.Bootstrap.Context;
using BC.Business.Author;
using BC.Infrastructure.Context;
using BC.Infrastructure.Interfaces.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Test.Services
{
    [TestClass]
    public class AuthorServiceTest
    {
       private IAuthorService authorService;

       [TestInitialize]
        public void Init()
        {
            MapperConfiguration mapperConfig = new DefaultMapperConfig().Configure();
            IMapper mapper = mapperConfig.CreateMapper();
            string connectionString = "";
            IRootContext rootContext = new RootContext(connectionString, mapper);
            
            authorService = new AuthorService(rootContext);
        }

        [TestMethod]
        public void RemoveTest()
        {

            authorService.Remove(1);
        }
    }
}
