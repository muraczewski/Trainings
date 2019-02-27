using BusinessLayer.Models;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IPersonService
    {
        Task<bool> TryAddPersonAsync(Person person, CancellationToken cancellationToken);

        Task<bool> TryUpdatePersonAsync(Person person, CancellationToken cancellationToken);

        Task<bool> TryRemovePersonAsync(int personId, CancellationToken cancellationToken);

        Task AddOrUpdatePersonAsync(Person person, CancellationToken cancellationToken);

        ConcurrentDictionary<int, Person> GetPeople();

        Person GetPerson(int id);

        PagedList<Person> GetPagedPeople(int pageIndex, int pageSize = 5);

        Task<bool> UpdateSurnameAsync(int id, string surname, CancellationToken cancellationToken);
    }
}
