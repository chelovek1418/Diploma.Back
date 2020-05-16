using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Interfaces
{
    public interface ICrudService<T> where T : class
    {
        IAsyncEnumerable<T> GetAllAsync();

        Task<int> CreateAsync(T model);

        Task<T> GetByIdAsync(int id);

        Task UpdateAsync(T model);

        Task DeleteAsync(int id);

        Task<int> GetCount();
    }
}
