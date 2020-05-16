using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Repository
{
    public class MarkRepository : AbstractRepository<Mark>, IMarkRepository
    {
        public MarkRepository(StudentPerfomanceDbContext context) : base(context)
        {
        }

        public IAsyncEnumerable<Mark> GetMarksForTimeByStudentId(int studentId, DateTime startDate, DateTime endDate) => dbContext.Marks
            .Include(x => x.Subject)
            .AsNoTracking()
            .Where(x => x.Date >= startDate && x.Date <= endDate && x.StudentId == studentId)
            .OrderByDescending(x => x.Date)
            .AsAsyncEnumerable();

        public IAsyncEnumerable<Mark> GetMarksByLessonForStudent(int studentId, int lessonId, DateTime startDate, DateTime endDate) => dbContext.Marks
            .Include(x => x.Subject)
            .AsNoTracking()
            .Where(x => x.Date >= startDate && x.StudentId == studentId && x.SubjectId == lessonId)
            .OrderByDescending(x => x.Date)
            .AsAsyncEnumerable();

        public async Task<double> GetAverageMark(DateTime startDate, DateTime endDate) => (await dbContext.Marks.Where(x => x.Date >= startDate && x.Date <= endDate).AverageAsync(x => x.Grade)) ?? 0;

        public async Task<double> GetAverageMarkByLessonForStudent(int studentId, int lessonId, DateTime startDate, DateTime endDate) =>
            (await dbContext.Marks.Where(x => x.StudentId == studentId && x.SubjectId == lessonId && x.Date >= startDate && x.Date <= endDate).AverageAsync(x => x.Grade)) ?? 0;

        public async Task<double> GetAverageMarkByLessonInGroup(int lessonId, int groupId, DateTime startDate, DateTime endDate) =>
            (await dbContext.Marks.Where(x => x.SubjectId == lessonId && x.Student.GroupId == groupId && x.Date >= startDate && x.Date <= endDate).AverageAsync(x => x.Grade)) ?? 0;

        public async Task<double> GetAverageMarkForLesson(int lessonId, DateTime startDate, DateTime endDate) =>
            (await dbContext.Marks.Where(x => x.SubjectId == lessonId && x.Date >= startDate && x.Date <= endDate).AverageAsync(x => x.Grade)) ?? 0;

        public async Task<double> GetAverageMarkForStudent(int studentId, DateTime startDate, DateTime endDate) =>
            (await dbContext.Marks.Where(x => x.StudentId == studentId && x.Date >= startDate && x.Date <= endDate).AverageAsync(x => x.Grade)) ?? 0;

        public async Task<double> GetAverageMarkInGroup(int groupId, DateTime startDate, DateTime endDate) =>
            (await dbContext.Marks.Where(x => x.Student.GroupId == groupId && x.Date >= startDate && x.Date <= endDate).AverageAsync(x => x.Grade)) ?? 0;
    }
}
