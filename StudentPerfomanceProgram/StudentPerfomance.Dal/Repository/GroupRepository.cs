using Microsoft.EntityFrameworkCore;
using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Repository
{
    public class GroupRepository : ContextProvider, IGroupRepository
    {
        public GroupRepository(StudentPerfomanceDbContext context) : base(context)
        {
        }

        public async Task AddLesson(int groupId, int lessonId)
        {
            await dbContext.GroupsLessons.AddAsync(new GroupsLessons { GroupId = groupId, LessonId = lessonId });
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException(nameof(title));

            return !await dbContext.Groups.AnyAsync(x => x.Title == title);
        }

        public async Task<int> CreateAsync(Groups model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(Groups));

            await dbContext.Groups.AddAsync(model);
            await dbContext.SaveChangesAsync();

            return model.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await dbContext.Groups.FindAsync(id);

            if (model == null)
                throw new NullReferenceException(nameof(Groups));

            dbContext.Groups.Remove(model);
            await dbContext.SaveChangesAsync();
        }

        public async Task DropLesson(int groupId, int lessonId)
        {
            var groupsLessons = await dbContext.GroupsLessons.FindAsync(groupId, lessonId);

            if (groupsLessons == null)
                throw new NullReferenceException(nameof(GroupsLessons));

            dbContext.GroupsLessons.Remove(groupsLessons);
            await dbContext.SaveChangesAsync();
        }

        public IAsyncEnumerable<Groups> GetAllAsync() => dbContext.Groups.AsAsyncEnumerable();

        public async Task<Groups> GetByIdAsync(int id)
        {
            var model = await dbContext.Groups
                .Include(x => x.Students)
                .ThenInclude(s=>s.IdNavigation)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
                throw new NullReferenceException(nameof(Groups));

            return model;
        }

        public IAsyncEnumerable<Groups> GetByLessonAsync(int id) => dbContext.Groups.Where(x => x.GroupsLessons.Any(y => y.LessonId == id)).AsNoTracking().AsAsyncEnumerable();

        public async Task<Groups> GetWithMarksByLesson(int groupId, int lessonId, DateTime date)
        {
            if (date.Month > DateTime.Today.Month)
                throw new ArgumentException(nameof(date));

            var group = await dbContext.Groups.Include(x => x.Students).ThenInclude(y => y.IdNavigation).FirstOrDefaultAsync(g => g.Id == groupId);

            if (group == null)
                throw new ArgumentException(nameof(groupId));

            var marks = await dbContext.Marks.Where(x => x.Student.GroupId == groupId && x.LessonId == lessonId && x.MarkDate.Year == date.Year && x.MarkDate.Month == date.Month).AsNoTracking().ToListAsync();
            foreach (var mark in marks)
                group.Students.FirstOrDefault(x => x.Id == mark.StudentId)?.Marks?.Add(mark);

            return group;
        }

        public IAsyncEnumerable<Groups> SearchAsync(string term) => dbContext.Groups.Where(x => x.Title.ToLower().Contains(term.ToLower())).AsNoTracking().Take(20).AsAsyncEnumerable();

        public async Task UpdateAsync(Groups model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(Groups));

            dbContext.Groups.Update(model);
            await dbContext.SaveChangesAsync();
        }
    }
}
