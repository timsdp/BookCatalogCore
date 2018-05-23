using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Data.Entity.Books
{
    public class BookEM
    {
        public int Id { get; set; }
        public DateTime DatePublished { get; set; }
        public string Name { get; set; }
        public int PagesCount { get; set; }
    }
}
