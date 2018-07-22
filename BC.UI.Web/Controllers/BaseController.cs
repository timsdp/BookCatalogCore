﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC.UI.Web.Filters;
using BC.UI.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BC.UI.Web.Controllers
{
    [CustomExceptionFilter]
    public class BaseController : Controller
    {
        public JsonResult GetBaseResponse(bool error, string message, object data=null, string[] messages=null)
        {
            return Json(new BaseResponse(error,message,data,messages));
        }
    }
}