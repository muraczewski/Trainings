using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using System.Collections.Concurrent;
using System.Threading;
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

        public async Task AddOrUpdatePersonAsync(Person person, CancellationToken cancellationToken)
        {
            await Task.Run(() => _people.AddOrUpdate(person.Id, person, null), cancellationToken);
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

        public async Task<bool> TryAddPersonAsync(Person person, CancellationToken cancellationToken)
        {
            var isSuccess = false;
            await Task.Run(() => isSuccess = _people.TryAdd(person.Id, person), cancellationToken);

            return isSuccess;
        }

        public async Task<bool> TryRemovePersonAsync(int personId, CancellationToken cancellationToken)
        {
            var isSuccess = false;
            await Task.Run(() => isSuccess = _people.TryRemove(personId, out Person removedPerson), cancellationToken);

            return isSuccess;
        }

        public async Task<bool> TryUpdatePersonAsync(Person person, CancellationToken cancellationToken)
        {
            _people.TryGetValue(person.Id, out var comparisonValue);

            var isSuccess = false;
            await Task.Run((() => isSuccess = _people.TryUpdate(person.Id, person, comparisonValue)), cancellationToken);

            return await Task.FromResult(isSuccess);
        }
    }
}
