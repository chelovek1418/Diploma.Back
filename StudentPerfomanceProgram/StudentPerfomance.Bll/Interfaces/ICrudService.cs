using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Interfaces
{
    public interface ICrudService<Q, T> where T : class where Q : class
    {
        IAsyncEnumerable<T> GetAllAsync(Func<Q, T> converter);

        Task<int> CreateAsync(T model, Func<T, Q> converter);

        Task<T> GetByIdAsync(int id, Func<Q, T> converter);

        Task UpdateAsync(T model, Func<T, Q> converter);

        Task DeleteAsync(int id);

        Task<int> GetCount();
    }
}
