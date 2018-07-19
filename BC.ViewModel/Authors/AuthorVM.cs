using BC.ViewModel.Books;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BC.ViewModel
{
    public class AuthorVM
    {
        public int? Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FullName { get; set; }
        [Required]
        public int Born { get; set; }
        public int Died { get; set; }
        public int Rating { get; set; }
        [Required]
        public string Country { get; set; }
        public string ExtraInfo { get; set; }
        [Required]
        [MinLength(20)]
        public string Quote { get; set; }
        public string Wiki { get; set; }
        public IEnumerable<BookVM> TopBooks { get; set; }
    }
}

