using BusinessLayer.Models;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IPersonService
    {
        Task TryAddPersonAsync(Person person);

        Task TryUpdatePersonAsync(Person person);

        Task TryRemovePersonAsync(int personId);

        Task AddOrUpdatePersonAsync(Person person);

        ConcurrentDictionary<int, Person> GetPeople();

        Person GetPerson(int id);
    }
}
