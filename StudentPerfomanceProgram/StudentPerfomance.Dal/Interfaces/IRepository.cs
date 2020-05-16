using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Interfaces
{
    public interface IRepository<T> where T : class, IDbEntity
    {
        IAsyncEnumerable<T> GetAllAsync();

        Task<IEnumerable<T>> FilterAsync(Func<T, bool> predicate);

        Task<int> CreateAsync(T model);

        Task<T> GetByIdAsync(int id);

        Task UpdateAsync(T model);

        Task DeleteAsync(int id);

        Task<int> GetCount();
    }
}
