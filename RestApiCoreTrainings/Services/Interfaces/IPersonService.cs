using BusinessLayer.Models;
using System.Collections.Generic;
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

        Task<Person> GetPersonAsync(int id, CancellationToken cancellationToken);

        Task<PagedList<Person>> GetPeopleAsync(int? pageIndex, int pageSize, CancellationToken cancellationToken);

        Task<PagedList2<Person>> GetPeopleAsync2(int? pageIndex, int pageSize, CancellationToken cancellationToken);
        
        Task<List<Person>> GetPeopleAsync(CancellationToken cancellationToken);

        Task<bool> UpdateSurnameAsync(int id, string surname, CancellationToken cancellationToken);
    }
}
