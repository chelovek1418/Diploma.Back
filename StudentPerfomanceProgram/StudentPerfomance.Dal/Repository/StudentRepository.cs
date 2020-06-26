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
    public class StudentRepository : AbstractRepository<Student>, IStudentRepository
    {
        public StudentRepository(StudentPerfomanceDbContext context) : base(context)
        {
        }

        public override async Task<int> CreateAsync(Student model)
        {
            if (model.User == null)
                throw new ArgumentNullException(nameof(model.User));

            return await base.CreateAsync(model);
        }

        public override async Task<Student> GetByIdAsync(int id)
        {
            var student = await dbContext.Students.Include(x => x.User).Include(x => x.Group).FirstOrDefaultAsync(x => x.Id == id);
            if (student == null)
                throw new NullReferenceException(nameof(Student));

            return student;
        }

        public override IAsyncEnumerable<Student> GetAllAsync() => dbContext.Students.Include(x => x.User).AsNoTracking().AsAsyncEnumerable();

        public override async Task<IEnumerable<Student>> FilterAsync(Expression<Func<Student, bool>> predicate) => await Task.Run(() => dbContext.Students.Include(x => x.User).Where(predicate));

        public override async Task DeleteAsync(int id)
        {
            var user = await dbContext.Users.FindAsync(id);

            if (user == null)
                throw new NullReferenceException(nameof(Teacher));

            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
        }

        public Task<Student> GetBestStudent(DateTime startDate, DateTime endDate)
        {
            //var bestMarkk = dbContext.Marks.Where(x => x.Date >= startDate && x.Date <= endDate).GroupBy(x => x.StudentId).FirstOrDefaultAsync(x => x..MaxAsync(x => x.Sum(y => y.Grade));
            throw new NotImplementedException();

            ////var student = dbContext.Students.Include(x => x.User).FirstOrDefaultAsync(x => x.Marks.Sum(m => m.Grade) >= dbContext.Students.)
        }

        public Task<Student> GetBestStudentForLesson(int lessonId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetBestStudentForLessonInGroup(int lessonId, int groupId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetBestStudentInGroup(int groupId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetTopStudents(DateTime startDate, DateTime endDate)
        {
            //var smstg = dbContext.Marks.GroupBy(x => new { x.StudentId, x.SubjectId });
            //List<object> list = new List<object>();
            //foreach (var item in smstg)
            //{
            //    item.Key.
            //}
            return await FilterAsync(x => x.Id > 0);
        }

        public Task<Student> GetWorstStudent(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetWorstStudentForLesson(int lessonId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetWorstStudentForLessonInGroup(int lessonId, int groupId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetWorstStudentInGroup(int groupId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
