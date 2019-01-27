using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class PersonService : IPersonService
    {
        private readonly ConcurrentDictionary<int, Person> _people;

        public PersonService()
        {
            _people = new ConcurrentDictionary<int, Person>();
        }

        public async Task AddOrUpdatePersonAsync(Person person)
        {
            await Task.Run(() => _people.AddOrUpdate(person.Id, person, null));
        }

        public ConcurrentDictionary<int, Person> GetPeople()
        {
            return _people;      
        }

        public Person GetPerson(int id)
        {
            var person = _people.GetOrAdd(id, new Person(id));
            return person;
        }

        public async Task TryAddPersonAsync(Person person)
        {
            await Task.Run(() => _people.TryAdd(person.Id, person)); 
        }

        public async Task TryRemovePersonAsync(int personId)
        {
            await Task.Run(() => _people.TryRemove(personId, out Person removedPerson));
        }

        public async Task TryUpdatePersonAsync(Person person)
        {
            await Task.Run(() => _people.TryAdd(person.Id, person));
        }
    }
}
