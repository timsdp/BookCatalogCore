using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BC.UI.Web.Controllers
{
    public class BaseController : Controller
    {
        public JsonResult response(int errorCode, string message)
        {
            return Json(new { Err = errorCode, Msg = message });
        }
    }
}