using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BC.UI.Web.Controllers
{
    public class AuthorsController : BaseController
    {
        AuthorsRepository authorsRepo = new AuthorsRepository();
        public IActionResult Index()
        {
            return View(authorsRepo.GetAuthors());
        }
    }
}