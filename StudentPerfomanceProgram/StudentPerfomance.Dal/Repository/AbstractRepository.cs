﻿using Microsoft.EntityFrameworkCore;
using StudentPerfomance.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Repository
{
    public abstract class AbstractRepository<TEntity> : IRepository<TEntity> where TEntity : class, IDbEntity
    {
        private protected StudentPerfomanceDbContext dbContext;
        protected AbstractRepository(StudentPerfomanceDbContext context)
        {
            dbContext = context;
        }

        public virtual async Task<int> CreateAsync(TEntity model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(TEntity));

            await dbContext.Set<TEntity>().AddAsync(model);
            await dbContext.SaveChangesAsync();

            return model.Id;
        }

        public virtual async Task DeleteAsync(int id)
        {
            var model = await dbContext.Set<TEntity>().FindAsync(id);

            if (model == null)
                throw new NullReferenceException(nameof(TEntity));

            dbContext.Set<TEntity>().Remove(model);
            await dbContext.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> FilterAsync(Expression<Func<TEntity, bool>> predicate) => await Task.Run(() => dbContext.Set<TEntity>().AsNoTracking().Where(predicate));

        public virtual IAsyncEnumerable<TEntity> GetAllAsync() => dbContext.Set<TEntity>().AsNoTracking().AsAsyncEnumerable();

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            var model = await dbContext.Set<TEntity>().FindAsync(id);
            if (model == null)
                throw new NullReferenceException(nameof(TEntity));

            return model;
        }

        public virtual async Task<int> GetCount() => await dbContext.Set<TEntity>().CountAsync();

        public virtual async Task UpdateAsync(TEntity model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(TEntity));

            dbContext.Set<TEntity>().Update(model);
            await dbContext.SaveChangesAsync();
        }
    }
}
