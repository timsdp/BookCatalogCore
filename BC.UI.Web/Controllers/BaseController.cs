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
        private IServiceProviderFactory currentFactory;
        private IRequestContext currentContext;

        public IServiceProviderFactory CurrentFactory
        {
            get
            {
                if (this.currentFactory == null)
                {
                    this.currentFactory = this.RequestContext.Factory;
                }
                return this.currentFactory;
            }
        }

        public IRequestContext RequestContext
        {
            get
            {

                if (this.currentContext == null)
                {
                    this.currentContext = new DefaultContext(base.HttpContext);
                }
                return this.currentContext;
            }

        }



        public JsonResult GetBaseResponse(bool error, string message, object data = null, List<string> messages = null)
        {
            string[] messagesArray = messages != null ? messages.ToArray() : null;
            return Json(new BaseResponse(error, message, data, messagesArray));
        }
    }
}