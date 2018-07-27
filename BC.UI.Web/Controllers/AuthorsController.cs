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
using BC.UI.Web.Models.Autocomplete;
using BC.UI.Web.Models.Datatable;
using BC.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BC.UI.Web.Controllers
{
    public class AuthorsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAutocomplete([FromQuery]AutocompleteRequest request)
        {
            if (string.IsNullOrEmpty(request.q)) return new JsonResult("");

            using (var service = this.CurrentContext.Factory.GetService<IAuthorService>(CurrentContext.RootContext))
            {
                Dictionary<int,string> entries = service.GetAutocomplete(request.q);
                var data = entries.Select(e => new { id = e.Key, text = e.Value}).ToArray();
                return new JsonResult(data);
            }
        }

        public JsonResult Get(int id)
        {
            using (var service = this.CurrentContext.Factory.GetService<IAuthorService>(CurrentContext.RootContext))
            {
                AuthorVM viewModel = service.GetById(id);
                return new JsonResult(viewModel);
            }
        }


        [HttpPost]
        public IActionResult Update(AuthorVM vm)
        {

                //if (authorService.CheckExist(vm))
                //{
                //    return response(1, "Author with provided Last and Firstname is already exists in DB");
                //}
                List<string> validationMessages = new List<string>();
            if (!ModelState.IsValid)
            {
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        validationMessages.Add(error.ErrorMessage);
                    }
                }
                return GetBaseResponse(true, "Server-side validation fails. Status = " + ModelState.ValidationState,null, validationMessages);
            }
            using (var service = this.CurrentContext.Factory.GetService<IAuthorService>(CurrentContext.RootContext))
            {
                service.Update(vm);
            }
           
            return GetBaseResponse(false,"Success");
        }

        [HttpPost]
        public JsonResult Remove(int id)
        {
            using(var service = this.CurrentContext.Factory.GetService<IAuthorService>(CurrentContext.RootContext))
            {
                service.Remove(id);
            }
            return GetBaseResponse(false, "Success");
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
                sortBy = model.columns[model.order[0].column].data;
                sortDir = model.order[0].dir.ToLower() == "asc";
            }

            var result = getData(searchBy, take, skip, sortBy, sortDir, out filteredResultsCount, out totalResultsCount);
            if (result == null)
            {
                return new List<AuthorVM>();
            }
            return result;
        }

        private List<AuthorVM> getData(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {

            if (String.IsNullOrEmpty(searchBy))
            {
                sortBy = "Id";
                sortDir = true;
            }

            IEnumerable<AuthorVM> entries = null;
            using (var service = this.CurrentContext.Factory.GetService<IAuthorService>(CurrentContext.RootContext))
            {
                entries = service.GetAllFiltered(searchBy, take, skip, sortBy, sortDir, out filteredResultsCount, out totalResultsCount);
            }
            
            return Mapper.Map<List<AuthorVM>>(entries); 
        }
        #endregion


    }
}