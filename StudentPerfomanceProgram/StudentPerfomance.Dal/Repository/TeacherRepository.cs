using Microsoft.EntityFrameworkCore;
using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Repository
{
    public class TeacherRepository : AbstractRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(StudentPerfomanceDbContext context) : base(context)
        {
        }

        public override async Task<int> CreateAsync(Teacher model)
        {
            if (model.User == null)
                throw new ArgumentNullException(nameof(model.User));

            model.IsConfirmed = false;
            return await base.CreateAsync(model);
        }

        public override async Task<Teacher> GetByIdAsync(int id)
        {
            var teacher = await dbContext.Teachers.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
            if (teacher == null)
                throw new NullReferenceException(nameof(Teacher));

            return teacher;
        }

        public override IAsyncEnumerable<Teacher> GetAllAsync() => dbContext.Teachers.Include(x => x.User).AsNoTracking().AsAsyncEnumerable();

        public override async Task<IEnumerable<Teacher>> FilterAsync(Expression<Func<Teacher, bool>> predicate) => await Task.Run(() => dbContext.Teachers.Include(x => x.User).Where(predicate));

        public override async Task DeleteAsync(int id)
        {
            var user = await dbContext.Users.FindAsync(id);

            if (user == null)
                throw new NullReferenceException(nameof(Teacher));

            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task AddLesson(int teacherId, int subjectId)
        {
            await dbContext.TeacherSubjects.AddAsync(new TeacherSubject { TeacherId = teacherId, SubjectId = subjectId });
            await dbContext.SaveChangesAsync();
        }

        public async Task DropLesson(int teacherId, int subjectId)
        {
            var teacherSubject = await dbContext.TeacherSubjects.FindAsync(teacherId, subjectId);

            if (teacherSubject == null)
                throw new NullReferenceException(nameof(teacherSubject));

            dbContext.TeacherSubjects.Remove(teacherSubject);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Teacher>> GetUnconfirmedTeachers() => await Task.Run(() => dbContext.Teachers.Include(x => x.User).IgnoreQueryFilters().Where(x => !x.IsConfirmed));

        public async Task ConfirmTeacher(int teacherId)
        {
            var teacher = await dbContext.Teachers.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == teacherId);

            if (teacher == null)
                throw new NullReferenceException(nameof(Teacher));

            teacher.IsConfirmed = true;
            await dbContext.SaveChangesAsync();
        }
    }
}
