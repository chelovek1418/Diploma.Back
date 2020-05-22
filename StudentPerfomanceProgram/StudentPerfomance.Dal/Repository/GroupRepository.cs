using Microsoft.EntityFrameworkCore;
using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Repository
{
    public class GroupRepository : AbstractRepository<Group>, IGroupRepository
    {
        public GroupRepository(StudentPerfomanceDbContext context) : base(context)
        {
        }

        public async Task AddLesson(int groupId, int subjectId)
        {
            await dbContext.GroupSubjects.AddAsync(new GroupSubject { GroupId = groupId, SubjectId = subjectId });
            await dbContext.SaveChangesAsync();
        }

        public async Task DropLesson(int groupId, int subjectId)
        {
            var groupSubj = await dbContext.GroupSubjects.FirstOrDefaultAsync(x => x.GroupId == groupId && x.SubjectId == subjectId);

            if (groupSubj == null)
                throw new NullReferenceException(nameof(GroupSubject));

            dbContext.GroupSubjects.Remove(groupSubj);
            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> CheckTitleAsync(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException(nameof(title));

            return !await dbContext.Groups.AnyAsync(x => x.Title == title);
        }

        public override async Task<Group> GetByIdAsync(int id)
        {
            var model = await dbContext.Groups
                .Include(x => x.Students)
                .ThenInclude(s => s.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (model == null)
                throw new NullReferenceException(nameof(Group));

            return model;
        }

        public async Task<Group> GetWithMarksByLesson(int groupId, int lessonId, DateTime date)
        {
            var group = await dbContext.Groups.Include(x => x.Students).ThenInclude(y => y.User).FirstOrDefaultAsync(g => g.Id == groupId);

            if (group == null)
                throw new ArgumentException(nameof(groupId));

            foreach (var mark in await dbContext.Marks.Where(x => x.Student.GroupId == groupId && x.SubjectId == lessonId && x.Date.Year == date.Year && x.Date.Month == date.Month).AsNoTracking().ToListAsync())
                group.Students.FirstOrDefault(x => x.Id == mark.StudentId)?.Marks?.Add(mark);

            return group;
        }
    }
}
