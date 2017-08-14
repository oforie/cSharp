using System.ComponentModel.DataAnnotations;
using System;

namespace LoginRegistration.Models

{
    public class RegisterUser: BaseEntity
    {
        [Required(ErrorMessage="Please enter a valid first name. Must be at least 2 characters")]
        [MinLength(2)]
        public string FirstName {get; set;}

        [Required(ErrorMessage="Please enter a valid last name. Must be at least 2 characters")]
        [MinLength(2)]
        public string LastName {get; set;}

        [Required(ErrorMessage="Please enter a valid email")]
        [EmailAddress]
        public string EmailAddress {get; set;}

        [Required]
        [DataType(DataType.Password)]
        public string Password {get; set;}

        [Compare("Password", ErrorMessage="Password and confirmation must match")]
        public string PasswordConfirmation {get; set;}  
    }
}