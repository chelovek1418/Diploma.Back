using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Interfaces;
using System;
using System.Collections.Generic;
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
            var lesson = await dbContext.Lessons.FindAsync(lessonId);
            var group = await dbContext.Groups.FindAsync(groupId);

            if (lesson == null)
                throw new NullReferenceException(nameof(Lessons));

            if (group == null)
                throw new NullReferenceException(nameof(Groups));

            await dbContext.GroupsLessons.AddAsync(new GroupsLessons { GroupId = groupId, LessonId = lessonId });
            await dbContext.SaveChangesAsync();
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
            var model = await dbContext.Groups.FindAsync(id);

            if (model == null)
                throw new NullReferenceException(nameof(Groups));

            return model;
        }

        public async Task UpdateAsync(Groups model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(Groups));

            dbContext.Groups.Update(model);
            await dbContext.SaveChangesAsync();
        }
    }
}
