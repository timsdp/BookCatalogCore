using BC.UI.Web.Controllers;
using BC.UI.Web.Models.Autocomplete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BC.Test.xUnit
{

    public class AuthorsControllerTest
    {
        [Fact]
        public void IndexTests()
        {
            AuthorsController controller = new AuthorsController();

            IActionResult result = controller.Index();
            ViewResult vewResult = (ViewResult)result;

            Assert.NotNull(vewResult);
        }

        [Fact]
        public void GetAutocompleteTests()
        {
            //Arrange
            AuthorsController controller = new AuthorsController();

            //Act
            IActionResult resultNull = controller.GetAutocomplete(null);
            IActionResult resultDefault = controller.GetAutocomplete(new AutocompleteRequest());
            IActionResult resultEmptyFields = controller.GetAutocomplete(new AutocompleteRequest() { q=string.Empty,term=string.Empty,_type=string.Empty});
            IActionResult resultSingleWord = controller.GetAutocomplete(new AutocompleteRequest() { q = "word", term = string.Empty, _type = string.Empty });
            IActionResult resultMultipleWords = controller.GetAutocomplete(new AutocompleteRequest() { q = "word search", term = string.Empty, _type = string.Empty });

            //Assert
            Assert.NotNull(resultNull);
            Assert.IsType<JsonResult>(resultNull);
            Assert.Equal(string.Empty, ((JsonResult)resultNull).Value);
                        
            Assert.NotNull(resultDefault);
            Assert.IsType<JsonResult>(resultDefault);
            Assert.Equal(string.Empty, ((JsonResult)resultDefault).Value);

            Assert.NotNull(resultEmptyFields);
            Assert.IsType<JsonResult>(resultEmptyFields);

            Assert.NotNull(resultSingleWord);
            Assert.IsType<JsonResult>(resultSingleWord);

            Assert.NotNull(resultMultipleWords);
            Assert.IsType<JsonResult>(resultMultipleWords);
        }
    }
}
