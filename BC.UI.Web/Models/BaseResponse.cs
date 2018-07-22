using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BC.UI.Web.Models
{
    public class BaseResponse
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public string[] Messages { get; set; }
        public object Data { get; set; }

        public BaseResponse(bool error, string message, object data, string[] messages = null)
        {
            this.Error = error;
            this.Message = message;
            this.Data = data;
            this.Messages = messages;
        }
    }

}
