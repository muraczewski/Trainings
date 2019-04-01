using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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

            for (int i = 1; i < 20; i++)
            {
                _people.TryAdd(i, new Person(i));
            }
        }

        public async Task AddOrUpdatePersonAsync(Person person, CancellationToken cancellationToken)
        {
            await Task.Run(() => _people.AddOrUpdate(person.Id, person, null));            
        }

        public async Task<Person> GetPersonAsync(int id, CancellationToken cancellationToken)
        {
            var person = _people.GetValueOrDefault(id);
            return await Task.FromResult(person);
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

            var isSuccess = _people.TryUpdate(person.Id, person, comparisonValue);

            return await Task.FromResult(isSuccess);
        }

        public async Task<bool> UpdateSurnameAsync(int id, string surname, CancellationToken cancellationToken)
        {
            try
            {
                _people.TryGetValue(id, out var comparisonValue);
                _people.TryGetValue(id, out var person);

                person.Surname = surname;

                var isSuccess = _people.TryUpdate(person.Id, person, comparisonValue);

                return await Task.FromResult(isSuccess);
            }
            catch (System.Exception)
            {
                return false;
            }
        }


        public async Task<PagedList<Person>> GetPeopleAsync(int? pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            if (pageIndex == null)
            {
                var allPeople = PagedList<Person>.GetPage(_people.Values, 1, _people.Count);
                return await Task.FromResult(allPeople);
            }

            var onePage = PagedList<Person>.GetPage(_people.Values, pageIndex.Value, pageSize);
            return await Task.FromResult(onePage);
        }

        public async Task<PagedList2<Person>> GetPeopleAsync2(int? pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var pagedResult = PagedList2<Person>.GetPage(_people.Values, pageIndex.Value, pageSize);
            return await Task.FromResult(pagedResult);
        }

        public async Task<List<Person>> GetPeopleAsync(CancellationToken cancellationToken)
        {
            var result = _people.Values.ToList();
            return await Task.FromResult(result);
        }
    }
}
