using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BC.Business.Author;
using BC.Business.Book;
using BC.Data.Entity.Authors;
using BC.Data.Repositories;
using BC.Infrastructure.Interfaces.Service;
using BC.UI.Web.Models.Datatable;
using BC.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BC.UI.Web.Controllers
{
    public class AuthorsController : BaseController
    {
        IBookService bookService;
        IAuthorService authorService;
        public AuthorsController(/*IBookService bookService, IAuthorService authorService*/)
        {
            this.bookService = bookService;
            this.authorService = authorService;

            string connString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=BookCatalog;Persist Security Info=True;User ID=sa;Password=Pa$$w0rd;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True";
            this.authorService = new AuthorService(connString);
            this.bookService = new BookService(connString);
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Get(int id)
        {
            AuthorVM viewModel = authorService.GetById(id);
            return new JsonResult(viewModel);
        }


        [HttpPost]
        public IActionResult Update(AuthorVM vm)
        {
            //if (authorService.CheckExist(vm))
            //{
            //    return response(1, "Author with provided Last and Firstname is already exists in DB");
            //}
            if (!ModelState.IsValid)
            {
                return response(1, "Server-side validation fails. Status = " + ModelState.ValidationState);
            }
            authorService.Update(vm);
            return response(0,"Success");
        }

        [HttpPost]
        public JsonResult Remove(int id)
        {
            authorService.Remove(id);
            return response(0, "Success");
        }


        [HttpPost]
        public JsonResult GetAll(DataTableAjaxPostModel model)
        {
            int filteredResultsCount;
            int totalResultsCount;
            var result = getAuthorsByCustomSearch(model, out filteredResultsCount, out totalResultsCount);

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
        private IList<AuthorVM> getAuthorsByCustomSearch(DataTableAjaxPostModel model, out int filteredResultsCount, out int totalResultsCount)
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
                return new List<AuthorVM>();
            }
            return result;
        }

        private List<AuthorVM> getDataFromDb(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            if (String.IsNullOrEmpty(searchBy))
            {
                // if we have an empty search then just order the results by Id ascending
                sortBy = "Id";
                sortDir = true;
            }

            var allEntries = authorService.GetAll();
            var filteredEntries = allEntries;

            if (!string.IsNullOrEmpty(searchBy))
            {
                filteredEntries = allEntries.Where(e => e.FirstName.Contains(searchBy) || e.LastName.Contains(searchBy)).ToList();
            }
            var pagedEntries = sortDir ? filteredEntries.OrderBy(e => e.FirstName).ToList() : filteredEntries.OrderByDescending(e => e.FirstName).ToList();
            pagedEntries = pagedEntries.Skip(skip).Take(take).ToList();

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            filteredResultsCount = filteredEntries.Count();
            totalResultsCount = allEntries.Count();
            var resultVm = Mapper.Map<List<AuthorVM>>(pagedEntries);
            return resultVm;
        }

        #endregion
    }
}