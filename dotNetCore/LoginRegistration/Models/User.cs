using System;

namespace LoginRegistration.Models

{
    public class User: BaseEntity
    {
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string EmailAddress {get; set;}
        public string Password {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
    }
}