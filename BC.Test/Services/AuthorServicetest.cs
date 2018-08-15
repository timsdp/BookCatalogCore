using AutoMapper;
using BC.Bootstrap;
using BC.Bootstrap.Context;
using BC.Business.Author;
using BC.Infrastructure.Context;
using BC.Infrastructure.DI;
using BC.Infrastructure.Interfaces.Service;
using BC.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
            //Act
            authorService.Remove(1);
            AuthorVM author = authorService.GetById(1);

            //Assert
            Assert.IsNull(author);
        }
    }
}
