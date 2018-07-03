using BC.Data.Entity.Authors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Data.Entity.Books
{
    public class BookEM
    {
        public int BookId { get; set; }
        public DateTime DatePublished { get; set; }
        public string Name { get; set; }
        public int PagesCount { get; set; }
        public int Rating { get; set; }
        public List<AuthorEM> Authors { get; set; }


        public BookEM()
        {
            this.Authors = new List<AuthorEM>();
        }
        public override string ToString()
        {
            return $"[{BookId}] {Name} ({Authors.Count})";
        }
    }
}
