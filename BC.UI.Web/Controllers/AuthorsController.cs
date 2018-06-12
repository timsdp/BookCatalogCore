﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BC.Data.Entity.Authors;
using BC.Data.Repositories;
using BC.UI.Web.Models.Datatable;
using BC.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BC.UI.Web.Controllers
{
    public class AuthorsController : BaseController
    {
        AuthorsRepository authorsRepo = new AuthorsRepository();
        public IActionResult Index()
        {
            //return View(authorsRepo.GetAuthors());
            //return View(new List<AuthorVM>());

            var authors = authorsRepo.GetAuthors();
            var vm = Mapper.Map<IEnumerable<AuthorVM>>(authors);
            return View(vm);
        }

        public IActionResult Edit(int id)
        {
            //var authorEM = authorsRepo.Get(id);
            //AuthorVM vm = Mapper.Map<AuthorVM>(authorEM);
            return View(new AuthorVM());
        }

        [HttpPost]
        public IActionResult Update(AuthorVM vm)
        {
            return Json(new { Err= 0, Msg = "Success"});
        }

        [HttpPost]
        public JsonResult GetAll(DataTableAjaxPostModel model)
        {// action inside a standard controller
            int filteredResultsCount;
            int totalResultsCount;
            var result = YourCustomSearchFunc(model, out filteredResultsCount, out totalResultsCount);

            return Json(new
            {
                // this is what datatables wants sending back
                draw = model.draw,
                recordsTotal = totalResultsCount,
                recordsFiltered = filteredResultsCount,
                data = result.ToArray()
            });
        }

        public IList<AuthorVM> YourCustomSearchFunc(DataTableAjaxPostModel model, out int filteredResultsCount, out int totalResultsCount)
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
            var result = GetDataFromDbase(searchBy, take, skip, sortBy, sortDir, out filteredResultsCount, out totalResultsCount);
            if (result == null)
            {
                // empty collection...
                return new List<AuthorVM>();
            }
            return result;
        }

        public List<AuthorVM> GetDataFromDbase(string searchBy, int take, int skip, string sortBy, bool sortDir, out int filteredResultsCount, out int totalResultsCount)
        {
            if (String.IsNullOrEmpty(searchBy))
            {
                // if we have an empty search then just order the results by Id ascending
                sortBy = "Id";
                sortDir = true;
            }

            var allEntries = authorsRepo.GetAuthors();
            var filteredEntries = allEntries;

            if (!string.IsNullOrEmpty(searchBy))
            {
                filteredEntries = allEntries.Where(e => e.FirstName.Contains(searchBy) || e.LastName.Contains(searchBy)).ToList();
            }
            var pagedEntries = sortDir ? filteredEntries.OrderBy(e => e.FirstName).ToList() : filteredEntries.OrderByDescending(e=>e.FirstName).ToList();
            pagedEntries = pagedEntries.Skip(skip).Take(take).ToList();

            // now just get the count of items (without the skip and take) - eg how many could be returned with filtering
            filteredResultsCount = filteredEntries.Count();
            totalResultsCount = allEntries.Count();
            var resultVm = Mapper.Map<List<AuthorVM>>(pagedEntries);
            return resultVm;
        }

    }
}