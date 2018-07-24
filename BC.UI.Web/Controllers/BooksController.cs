using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BC.Business.Author;
using BC.Business.Book;
using BC.Data.Entity.Books;
using BC.Data.Repositories;
using BC.Infrastructure.Interfaces.Service;
using BC.UI.Web.Models.Datatable;
using BC.ViewModel.Books;
using Microsoft.AspNetCore.Mvc;

namespace BC.UI.Web.Controllers
{
    public class BooksController : BaseController
    {
        IBookService bookService;
        IAuthorService authorService;
        public BooksController(/*IBookService bookService, IAuthorService authorService*/)
        {
            //this.bookService = bookService;
            //this.authorService = authorService;

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
            BookVM viewModel = bookService.GetById(id);
            return new JsonResult(viewModel);
        }

        [HttpPost]
        public JsonResult Remove(int id)
        {
            bookService.Remove(id);
            return GetBaseResponse(false, "Success");
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
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }

            var result = getData(searchBy, take, skip, sortBy, sortDir, out filteredResultsCount, out totalResultsCount);
            if (result == null)
            {
                return new List<BookVM>();
            }
            return result;
        }

        private List<BookVM> getData(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            if (String.IsNullOrEmpty(searchBy))
            {
                sortBy = "Id";
                sortDir = true;
            }

            var entries = bookService.GetAllFiltered(searchBy,take,skip,sortBy,sortDir,out filteredResultsCount,out totalResultsCount);

            var resultVm = Mapper.Map<List<BookVM>>(entries);
            return resultVm;
        }
        #endregion
    }
}