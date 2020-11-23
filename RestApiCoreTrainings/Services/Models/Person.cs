using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BusinessLayer.Attributes;
using BusinessLayer.Attributes.Validation;

namespace BusinessLayer.Models
{
    [BugReport("There are no validation", "1234")]
    public class Person
    {
        
        public Person()
        {
        }

        public Person(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        [EmailAddress(ErrorMessage = "Email is not valid")]
        [EmailDomainValidation("pgs-soft.com", ErrorMessage = "Email domain must be pgs-soft.com")]
        public string Email { get; set; }

        [BugReport("There is no validation for Age lower than 0")]
        [BugReport("There is no validation for Age higher than 130")]
        public int Age { get; set; }
    }
}
