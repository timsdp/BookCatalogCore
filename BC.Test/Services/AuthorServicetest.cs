using AutoMapper;
using BC.Bootstrap;
using BC.Bootstrap.Context;
using BC.Business.Author;
using BC.Business.FakeData.Repositories;
using BC.Data.Entity.Authors;
using BC.Data.Repositories;
using BC.Infrastructure.Context;
using BC.Infrastructure.DI;
using BC.Infrastructure.Interfaces.Repository;
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
        IAuthorRepository repository;
        IAuthorService service;

        [TestInitialize]
        public void TestInit()
        {
            repository = new FakeAuthorRepository();
            

            //Factory mock
            var factoryMock = new Mock<IServiceProviderFactory>();
            factoryMock.Setup(f => f.GetService<IAuthorRepository>(It.IsAny<IRootContext>())).Returns(repository);

            //Root context mock
            var rootContextMock = new Mock<IRootContext>();
            rootContextMock.Setup(f => f.Factory).Returns(factoryMock.Object);

            service = new AuthorService(rootContextMock.Object);
            
        }

        [TestMethod]
        public void CheckExistAuthorReallyExists()
        {
            //Arrange
            repository.Create(new AuthorEM() { FirstName = "Max", LastName = "Smith", YearBorn = 1900 });
            repository.Create(new AuthorEM() { FirstName = "Kelly", LastName = "Somers", YearBorn = 1950 });

            //Act
            bool resultAuthor1FirstLetter = service.CheckExist(new AuthorVM() { FirstName = "Max", LastName = "Smith", Born = 1900 });
            bool resultAuthor1LowerCase = service.CheckExist(new AuthorVM() { FirstName = "max", LastName = "smith", Born = 1900 });
            bool resultAuthor1UpperCase = service.CheckExist(new AuthorVM() { FirstName = "MAX", LastName = "SMITH", Born = 1900 });
            bool resultAuthor1MixedCase = service.CheckExist(new AuthorVM() { FirstName = "MaX", LastName = "SmItH", Born = 1900 });



            //Assert
           Assert.IsTrue(resultAuthor1FirstLetter);
           Assert.IsTrue(resultAuthor1LowerCase  );
           Assert.IsTrue(resultAuthor1UpperCase  );
           Assert.IsTrue(resultAuthor1MixedCase);
        }
        [TestMethod]
        public void CheckExistAuthorDoesntExist()
        {
            //Arrange


            //Act
            bool result = service.CheckExist(new AuthorVM() { FirstName = "Unknown", LastName = "Author", Born = 9999 });

            //Assert
            Assert.IsFalse(result);
        }
    }
}
