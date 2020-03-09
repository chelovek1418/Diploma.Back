using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IAsyncEnumerable<T> GetAllAsync();

        Task<int> CreateAsync(T model);

        Task<T> GetByIdAsync(int id);

        Task UpdateAsync(T model);

        Task DeleteAsync(int id);
    }
}
