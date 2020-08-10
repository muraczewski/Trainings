using BusinessLayer.Attributes;

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

        [BugReport("There is no validation for Age lower than 0")]
        [BugReport("There is no validation for Age higher than 130")]
        public int Age { get; set; }
    }
}
