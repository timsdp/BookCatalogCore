using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BC.UI.Web.Models.Autocomplete
{
    public class AutocompleteResponse
    {
        private int id;
        private string title;

        public AutocompleteResponse(int id, string text)
        {
            this.id = id;
            this.title = text;
        }
    }
}
