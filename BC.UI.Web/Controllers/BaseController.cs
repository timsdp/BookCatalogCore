using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC.UI.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace BC.UI.Web.Controllers
{
    [CustomExceptionFilter]
    public class BaseController : Controller
    {
        public JsonResult response(int errorCode, string message)
        {
            return Json(new { Err = errorCode, Msg = message });
        }
    }
}