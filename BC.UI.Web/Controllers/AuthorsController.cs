using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BC.Data.Entity.Authors;
using BC.Data.Repositories;
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
            return View(new List<AuthorEM>());

        }

        public IActionResult Edit(int id)
        {
            var authorEM = authorsRepo.Get(id);
            AuthorVM vm = Mapper.Map<AuthorVM>(authorEM);
            return View(vm);
        }

    }
}