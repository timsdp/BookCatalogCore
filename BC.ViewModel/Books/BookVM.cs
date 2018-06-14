using System;
using System.Collections.Generic;
using System.Text;

namespace BC.ViewModel.Books
{
    public class BookVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime Published { get; set; }
        public int Pages { get; set; }
        public int Rating { get; set; }
        public List<AuthorVM> Authors { get; set; }
    }
}
