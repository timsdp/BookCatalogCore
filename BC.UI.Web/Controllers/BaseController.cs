using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC.Infrastructure.Interfaces.Base;
using BC.UI.Web.Filters;
using BC.UI.Web.Models;
using BC.UI.Web.Models.Context;
using Microsoft.AspNetCore.Mvc;

namespace BC.UI.Web.Controllers
{
    [CustomExceptionFilter]
    public class BaseController : Controller
    {
        public JsonResult GetBaseResponse(bool error, string message, object data = null, List<string> messages = null)
        {
            string[] messagesArray = messages != null ? messages.ToArray() : null;
            return Json(new BaseResponse(error, message, data, messagesArray));
        }
    }
}