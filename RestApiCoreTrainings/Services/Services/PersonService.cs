using System;
using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Services
{
    public class PersonService : IPersonService
    {
        private readonly ILogger<PersonService> _logger;
        private readonly ConcurrentDictionary<int, Person> _people;

        public PersonService(ILogger<PersonService> logger)
        {
            _logger = logger;
            _people = new ConcurrentDictionary<int, Person>();
            
/*            for (int i = 1; i < 20; i++)
            {
                _people.TryAdd(i, new Person(i));
            }*/
        }

        public async Task<Person> GetPersonAsync(int id, CancellationToken cancellationToken)
        {
            var person = _people.GetValueOrDefault(id);
            _logger.LogInformation("Message1 {Person.Age} {Person.ID}", person.Age, person.Id, person);
            return await Task.FromResult(person);
        }

        public async Task<bool> TryAddPersonAsync(Person person, CancellationToken cancellationToken)
        {
            var isSuccess = false;
            await Task.Run(() => isSuccess = _people.TryAdd(person.Id, person), cancellationToken);

            return isSuccess;
        }

        public async Task AddPersonAsync(Person person, CancellationToken cancellationToken)
        {
            await Task.Run(() => _people.TryAdd(person.Id, person), cancellationToken);
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

        public async Task<PagedList<Person>> GetPeopleAsync(int pageIndex, int pageSize, CancellationToken cancellationToken)
        {
            var onePage = PagedList<Person>.GetPage(_people.Values, pageIndex, pageSize);
            return await Task.FromResult(onePage);
        }
    }
}
