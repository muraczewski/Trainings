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

        public async Task<bool> TryAddPersonAsync(Person person)
        {
            var isSuccess = false;
            await Task.Run(() => isSuccess = _people.TryAdd(person.Id, person));

            return isSuccess;
        }

        public async Task<bool> TryRemovePersonAsync(int personId)
        {
            var isSuccess = false;
            await Task.Run(() => isSuccess = _people.TryRemove(personId, out Person removedPerson));

            return isSuccess;
        }

        public async Task<bool> TryUpdatePersonAsync(Person person)
        {
            var isSuccess = false;

            _people.TryGetValue(person.Id, out var comparisonValue);
            await Task.Run(() => isSuccess = _people.TryUpdate(person.Id, person, comparisonValue));

            return isSuccess;
        }
    }
}
