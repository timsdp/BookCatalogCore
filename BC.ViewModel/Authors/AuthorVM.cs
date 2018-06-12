using System;

namespace BC.ViewModel
{
    public class AuthorVM
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int Born { get; set; }
        public int Died { get; set; }
        public int Rating { get; set; }
        public string Country { get; set; }
        public string ExtraInfo { get; set; }
        public string Quote { get; set; }
        public string Wiki { get; set; }
    }
}

