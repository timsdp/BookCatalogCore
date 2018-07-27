using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BC.UI.Web.Models.Autocomplete
{
    public class AutocompleteRequest
    {
        public string term { get; set; }
        public string q { get; set; }
        public string _type { get; set; }
    }
}
