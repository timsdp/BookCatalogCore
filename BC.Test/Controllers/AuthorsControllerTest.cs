using AutoMapper;
using BC.Bootstrap;
using BC.Bootstrap.Context;
using BC.Business.Author;
using BC.Data.Entity.Authors;
using BC.Infrastructure.Context;
using BC.Infrastructure.DI;
using BC.Infrastructure.Interfaces.Repository;
using BC.Infrastructure.Interfaces.Service;
using BC.UI.Web.Controllers;
using BC.UI.Web.Models.Autocomplete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Test.Controllers
{
    [TestClass]
    public class AuthorsControllerTest
    {
        AuthorsController authorController;

        [TestInitialize]
        public void TestInit()
        {
            //Repo mock
            var authorRepoMock = new Mock<IAuthorRepository>();
            authorRepoMock.Setup(repo => repo.Get(It.IsAny<int>())).Returns(() => null);
            authorRepoMock.Setup(repo => repo.GetAutocomplete(It.IsAny<string>())).Returns(() => new Dictionary<int, string>());

            //Svc mock
            var authorSvcMock = new Mock<IAuthorService>();
            authorSvcMock.Setup(f => f.GetAutocomplete(It.IsAny<string>())).Returns(new Dictionary<int, string>());

            //Factory mock
            var factoryMock = new Mock<IServiceProviderFactory>();
            factoryMock.Setup(f => f.GetService<IAuthorRepository>(It.IsAny<IRootContext>())).Returns(authorRepoMock.Object);
            factoryMock.Setup(f => f.GetService<IAuthorService>(It.IsAny<IRootContext>())).Returns(authorSvcMock.Object);
            
            //Context
            //MapperConfiguration mapperConfig = new DefaultMapperConfig().Configure();
            //IMapper mapper = mapperConfig.CreateMapper();
            
            //Root context mock
            var rootContextMock = new Mock<IRootContext>();
            rootContextMock.Setup(f => f.Factory).Returns(factoryMock.Object);

            //Request context mock
            var requestContextMock = new Mock<IRequestContext>();
            requestContextMock.Setup(f => f.RootContext).Returns(rootContextMock.Object);
            requestContextMock.Setup(f => f.Factory).Returns(factoryMock.Object);

            //Author controller mock
            var authorControllerMock = new Mock<AuthorsController>();
            authorControllerMock.Setup(f => f.CurrentContext).Returns(requestContextMock.Object);
            authorController = authorControllerMock.Object;
        }



        [TestMethod]
        public void IndexTests()
        {
            //Mocking
            var mock = new Mock<IAuthorRepository>();
            mock.Setup(repo => repo.Get()).Returns(new List<AuthorEM>() { new AuthorEM() });

            AuthorsController controller = new AuthorsController();

            IActionResult result = controller.Index();
            ViewResult vewResult = (ViewResult)result;

            Assert.IsNotNull(vewResult);
        }

        [TestMethod]
        public void GetAutocompleteTests()
        {
            //Arrange
            AuthorsController controller = this.authorController;

            //Act
            IActionResult resultNull = controller.GetAutocomplete(null);
            IActionResult resultDefault = controller.GetAutocomplete(new AutocompleteRequest());
            IActionResult resultEmptyFields = controller.GetAutocomplete(new AutocompleteRequest() { q=string.Empty,term=string.Empty,_type=string.Empty});
            IActionResult resultSingleWord = controller.GetAutocomplete(new AutocompleteRequest() { q = "word", term = string.Empty, _type = string.Empty });
            IActionResult resultMultipleWords = controller.GetAutocomplete(new AutocompleteRequest() { q = "word search", term = string.Empty, _type = string.Empty });

            //Assert
            Assert.IsNotNull(resultNull);
            Assert.IsInstanceOfType(resultNull, typeof(JsonResult));
            Assert.AreEqual(string.Empty, ((JsonResult)resultNull).Value);
                        
            Assert.IsNotNull(resultDefault);
            Assert.IsInstanceOfType(resultDefault, typeof(JsonResult));
            Assert.AreEqual(string.Empty, ((JsonResult)resultDefault).Value);

            Assert.IsNotNull(resultEmptyFields);
            Assert.IsInstanceOfType(resultEmptyFields, typeof(JsonResult));

            Assert.IsNotNull(resultSingleWord);
            Assert.IsInstanceOfType(resultSingleWord, typeof(JsonResult));

            Assert.IsNotNull(resultMultipleWords);
            Assert.IsInstanceOfType(resultMultipleWords, typeof(JsonResult));
        }
    }
}
