using BusinessLayer.Models;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IPersonService
    {
        Task<bool> TryAddPersonAsync(Person person);

        Task<bool> TryUpdatePersonAsync(Person person);

        Task<bool> TryRemovePersonAsync(int personId);

        Task AddOrUpdatePersonAsync(Person person);

        ConcurrentDictionary<int, Person> GetPeople();

        Person GetPerson(int id);
    }
}
