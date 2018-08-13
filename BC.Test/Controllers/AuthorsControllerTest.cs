using BC.UI.Web.Controllers;
using BC.UI.Web.Models.Autocomplete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BC.Test.Controllers
{
    [TestClass]
    public class AuthorsControllerTest
    {
        [TestMethod]
        public void IndexTests()
        {
            AuthorsController controller = new AuthorsController();

            IActionResult result = controller.Index();
            ViewResult vewResult = (ViewResult)result;

            Assert.IsNotNull(vewResult);
        }

        [TestMethod]
        public void GetAutocompleteTests()
        {
            AuthorsController controller = new AuthorsController();

            IActionResult resultNull = controller.GetAutocomplete(null);
            IActionResult resultDefault = controller.GetAutocomplete(new AutocompleteRequest());
            IActionResult resultEmptyFields = controller.GetAutocomplete(new AutocompleteRequest() { q=string.Empty,term=string.Empty,_type=string.Empty});
            IActionResult resultSingleWord = controller.GetAutocomplete(new AutocompleteRequest() { q = "word", term = string.Empty, _type = string.Empty });
            IActionResult resultMultipleWords = controller.GetAutocomplete(new AutocompleteRequest() { q = "word search", term = string.Empty, _type = string.Empty });

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
