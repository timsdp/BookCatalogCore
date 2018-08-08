using BC.UI.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Test.Controllers
{
    [TestClass]
    public class HomeControllerClass
    {
        [TestMethod]
        public void HomeIndexTests()
        {
            //Arrange
            HomeController controller = new HomeController();

            //Act
            IActionResult result = controller.Index();
            ViewResult vewResult = (ViewResult)result;

            //Assert
            Assert.AreEqual("Hello world!", vewResult.ViewData["Message"]);
        }
    }
}
