using StudentPerfomance.Bll.Interfaces;
using StudentPerfomance.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Services
{
    public abstract class AbstractCrudService<QEntity, TEntity> : ICrudService<QEntity, TEntity> where TEntity : class where QEntity : class, IDbEntity
    {
        protected private readonly IRepository<QEntity> _repository;

        protected AbstractCrudService(IRepository<QEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task<int> CreateAsync(TEntity model, Func<TEntity, QEntity> converter) => await _repository.CreateAsync(converter?.Invoke(model));

        public virtual async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public virtual async IAsyncEnumerable<TEntity> GetAllAsync(Func<QEntity, TEntity> converter)
        {
            await foreach (var entity in _repository.GetAllAsync())
                yield return converter?.Invoke(entity);
        }

        public virtual async Task<TEntity> GetByIdAsync(int id, Func<QEntity, TEntity> converter) => converter?.Invoke(await _repository.GetByIdAsync(id));

        public virtual async Task<int> GetCount() => await _repository.GetCount();

        public virtual async Task UpdateAsync(TEntity model, Func<TEntity, QEntity> converter) => await _repository.UpdateAsync(converter?.Invoke(model));
    }
}
