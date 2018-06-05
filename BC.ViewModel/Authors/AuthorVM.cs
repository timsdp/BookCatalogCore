using System;

namespace BC.ViewModel
{
    public class AuthorVM
    {
        public int? AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int YearBorn { get; set; }
        public int YearDied { get; set; }
        public int Rating { get; set; }
        public string Country { get; set; }
        public string ExtraInfo { get; set; }
        public string Quote { get; set; }
    }
}
