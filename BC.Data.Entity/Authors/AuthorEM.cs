using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Data.Entity.Authors
{
    public class AuthorEM
    {
        public int? AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int YearBorn { get; set; }
        public int YearDied { get; set; }
        public int Rating { get; set; }
        public string Country { get; set; }
        public string ExtraInfo { get; set; }
        public string Quote { get; set; }

        public override string ToString()
        {
            return $"[{AuthorId}] {FirstName} {LastName} ({YearBorn-YearDied})";
        }
    }
}
