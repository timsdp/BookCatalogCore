using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC.Bootstrap.Context;
using BC.Infrastructure.Context;
using BC.UI.Web.Filters;
using BC.UI.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols;

namespace BC.UI.Web.Controllers
{
    [CustomExceptionFilter]
    public class BaseController : Controller
    {
        #region Constructors
        public BaseController()
        {
            CurrentContext = new WebContext(@"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=BookCatalog;Persist Security Info=True;User ID=sa;Password=Pa$$w0rd;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True");
        }
        #endregion

        protected IWebContext CurrentContext { get; }


        public JsonResult GetBaseResponse(bool error, string message, object data = null, List<string> messages = null)
        {
            string[] messagesArray = messages != null ? messages.ToArray() : null;
            return Json(new BaseResponse(error, message, data, messagesArray));
        }
    }
}