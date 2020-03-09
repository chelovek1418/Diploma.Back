using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Repository
{
    public class LessonRepository : ContextProvider, IRepository<Lessons>
    {
        public LessonRepository(StudentPerfomanceDbContext context) : base(context)
        {
        }

        public async Task<int> CreateAsync(Lessons model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(Lessons));

            await dbContext.Lessons.AddAsync(model);
            await dbContext.SaveChangesAsync();

            return model.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await dbContext.Lessons.FindAsync(id);

            if (model == null)
                throw new NullReferenceException(nameof(Lessons));

            dbContext.Lessons.Remove(model);
            await dbContext.SaveChangesAsync();
        }

        public IAsyncEnumerable<Lessons> GetAllAsync() => dbContext.Lessons.AsAsyncEnumerable();

        public async Task<Lessons> GetByIdAsync(int id)
        {
            var model = await dbContext.Lessons.FindAsync(id);

            if (model == null)
                throw new NullReferenceException(nameof(Lessons));

            return model;
        }

        public async Task UpdateAsync(Lessons model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(Lessons));

            dbContext.Lessons.Update(model);
            await dbContext.SaveChangesAsync();
        }
    }
}
