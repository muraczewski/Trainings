using BusinessLayer.Models;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IPersonService
    {
        Task<bool> TryAddPersonAsync(Person person, CancellationToken cancellationToken);

        Task AddPersonAsync(Person person, CancellationToken cancellationToken);

        Task<bool> TryUpdatePersonAsync(Person person, CancellationToken cancellationToken);

        Task<bool> TryRemovePersonAsync(int personId, CancellationToken cancellationToken);

        Task<Person> GetPersonAsync(int id, CancellationToken cancellationToken);

        Task<PagedList<Person>> GetPeopleAsync(int pageIndex, int pageSize, CancellationToken cancellationToken);

        Task<bool> UpdateSurnameAsync(int id, string surname, CancellationToken cancellationToken);
    }
}
