using System;

namespace BC.ViewModel
{
    public class AuthorVM
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName { get; set; }
        public string DateOfBirth { get; set; }
    }
}
