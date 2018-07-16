using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BC.Data.Entity.Books;
using BC.Data.Repositories;
using BC.UI.Web.Models.Datatable;
using BC.ViewModel.Books;
using Microsoft.AspNetCore.Mvc;

namespace BC.UI.Web.Controllers
{
    public class BooksController : Controller
    {
        BooksRepository booksRepo = new BooksRepository();
        public IActionResult Index()
        {
            return View();
        }



        public IActionResult Edit(int id)
        {
            return View("_Edit");
        }

        [HttpPost]
        public JsonResult GetAll(DataTableAjaxPostModel model)
        {
            int filteredResultsCount;
            int totalResultsCount;
            var result = getBooksByCustomSearch(model, out filteredResultsCount, out totalResultsCount);

            return Json(new
            {
                // this is what datatables wants sending back, it uses to diferentiate ajax calls and responses
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result.ToArray()
            });
        }


        #region DataTables methods
        private IList<BookVM> getBooksByCustomSearch(DataTableAjaxPostModel model, out int filteredResultsCount, out int totalResultsCount)
        {
            var searchBy = (model.search != null) ? model.search.value : null;
            var take = model.length;
            var skip = model.start;

            string sortBy = "";
            bool sortDir = true;

            if (model.order != null)
            {
                // in this example we just default sort on the 1st column
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }

            // search the dbase taking into consideration table sorting and paging
            var result = getDataFromDb(searchBy, take, skip, sortBy, sortDir, out filteredResultsCount, out totalResultsCount);
            if (result == null)
            {
                // empty collection...
                return new List<BookVM>();
            }
            return result;
        }

        private List<BookVM> getDataFromDb(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            if (String.IsNullOrEmpty(searchBy))
            {
                // if we have an empty search then just order the results by Id ascending
                sortBy = "Id";
                sortDir = true;
            }

            var entries = booksRepo.GetAll(searchBy,take,skip,sortBy,sortDir,out filteredResultsCount,out totalResultsCount);

            var resultVm = Mapper.Map<List<BookVM>>(entries);
            return resultVm;
        }
        #endregion
    }
}