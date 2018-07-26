using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BC.UI.Web.Models
{
    public class ExceptionResponse : BaseResponse
    {
        public string Exception { get; set; }
        public ExceptionResponse(string exception, bool error, string message, object data=null, string[] messages = null) : base (error,message,data,messages)
        {
            this.Exception = exception;
        }
    }

}
