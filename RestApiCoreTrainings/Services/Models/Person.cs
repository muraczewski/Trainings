namespace BusinessLayer.Models
{
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

        public int Age { get; set; }
    }
}
