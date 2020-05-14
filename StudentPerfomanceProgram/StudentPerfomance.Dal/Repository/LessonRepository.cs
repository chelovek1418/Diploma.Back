using Microsoft.EntityFrameworkCore;
using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace StudentPerfomance.Dal.Repository
{
    public class LessonRepository : ContextProvider, ILessonRepository
    {
        public LessonRepository(StudentPerfomanceDbContext context) : base(context)
        {
        }

        public async Task<bool> CheckTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException(nameof(title));

            return !await dbContext.Lessons.AnyAsync(x => x.Title == title);
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

        public IAsyncEnumerable<Lessons> GetAllAsync() => dbContext.Lessons.AsNoTracking().AsAsyncEnumerable();

        public async Task<Lessons> GetByIdAsync(int id)
        {
            var model = await dbContext.Lessons.FindAsync(id);

            if (model == null)
                throw new NullReferenceException(nameof(Lessons));

            return model;
        }

        public IAsyncEnumerable<Lessons> GetLessonsByGroup(int groupId) => dbContext.GroupsLessons.Where(y => y.GroupId == groupId).Select(x=>x.Lesson).AsAsyncEnumerable();

        public async Task<IEnumerable<Lessons>> GetLessonsWithMarksForTimeByStudentId(int studentId, DateTime startDate, DateTime endDate)
        {
            var lessons = await dbContext.Lessons
            .Include(l => l.Marks)
            .Where(l => l.GroupsLessons.Any(g => g.Group.Students.Any(s => s.Id == studentId))).ToListAsync();
            lessons.ForEach(x => x.Marks = x.Marks.Where(m => m.StudentId == studentId && m.MarkDate >= startDate && m.MarkDate <= endDate).ToList());
            return lessons;
        }

        public IAsyncEnumerable<Lessons> SearchAsync(string term) => dbContext.Lessons.Where(x => x.Title.ToLower().Contains(term.ToLower())).AsNoTracking().AsAsyncEnumerable();

        public async Task UpdateAsync(Lessons model)
        {
            if (model == null)

                throw new ArgumentNullException(nameof(Lessons));

            dbContext.Lessons.Update(model);
            await dbContext.SaveChangesAsync();
        }
    }
}
