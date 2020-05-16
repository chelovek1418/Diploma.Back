using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Interfaces
{
    public abstract class AbstractCrudService<TEntity> : ICrudService<TEntity> where TEntity : class
    {
        public Task<int> CreateAsync(TEntity model)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<TEntity> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> GetCount()
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(TEntity model)
        {
            throw new System.NotImplementedException();
        }
    }
}
