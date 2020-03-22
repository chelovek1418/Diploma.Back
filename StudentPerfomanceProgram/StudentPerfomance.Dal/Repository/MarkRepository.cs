using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentPerfomance.Dal.Constants;
using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Repository
{
    public class MarkRepository : ContextProvider, IMarkRepository
    {
        public MarkRepository(StudentPerfomanceDbContext context) : base(context)
        {
        }

        public async Task<int> CreateAsync(Marks model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(Marks));

            await dbContext.Marks.AddAsync(model);
            await dbContext.SaveChangesAsync();

            return model.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await dbContext.Marks.FindAsync(id);

            if (model == null)
                throw new NullReferenceException(nameof(Marks));

            dbContext.Marks.Remove(model);
            await dbContext.SaveChangesAsync();
        }

        public IAsyncEnumerable<Marks> GetAllAsync() => dbContext.Marks.AsAsyncEnumerable();

        public async Task<double> GetAverageMarkByLessonForStudent(int studentId, int lessonId)
        {
            var studentIdparam = new SqlParameter("@studentId", studentId);
            var lessonIdparam = new SqlParameter("@lessonId", lessonId);
            var outParam = new SqlParameter
            {
                ParameterName = "@averageMark",
                SqlDbType = System.Data.SqlDbType.Float,
                Direction = System.Data.ParameterDirection.Output,
            };
            await Task.Run(() => dbContext.Database.ExecuteSqlRaw(StoredProcedures.GetAverageMarkForLessonForStudentProcedure + " @studentId, @lessonId, @averageMark OUT", studentIdparam, lessonIdparam, outParam));

            if (!(outParam.Value is double))
                throw new NullReferenceException(nameof(Marks));

            return (double)outParam.Value;
        }

        public async Task<double> GetAverageMarkByLessonInGroup(int lessonId, int groupId)
        {
            var lessonIdparam = new SqlParameter("@lessonId", lessonId);
            var groupIdparam = new SqlParameter("@groupId", groupId);
            var outParam = new SqlParameter
            {
                ParameterName = "@averageMark",
                SqlDbType = System.Data.SqlDbType.Float,
                Direction = System.Data.ParameterDirection.Output,
            };
            await Task.Run(() => dbContext.Database.ExecuteSqlRaw(StoredProcedures.GetAverageMarkForLessonInGroupProcedure + " @lessonId, @groupId, @averageMark OUT", lessonIdparam, groupIdparam, outParam));

            if (!(outParam.Value is double))
                throw new NullReferenceException(nameof(Marks));

            return (double)outParam.Value;
        }

        public async Task<double> GetAverageMarkForLesson(int lessonId)
        {
            var lessonIdparam = new SqlParameter("@lessonId", lessonId);
            var outParam = new SqlParameter
            {
                ParameterName = "@averageMark",
                SqlDbType = System.Data.SqlDbType.Float,
                Direction = System.Data.ParameterDirection.Output,
            };
            await Task.Run(() => dbContext.Database.ExecuteSqlRaw(StoredProcedures.GetAverageMarkForLessonProcedure + " @lessonId, @averageMark OUT", lessonIdparam, outParam));

            if (!(outParam.Value is double))
                throw new NullReferenceException(nameof(Marks));

            return (double)outParam.Value;
        }

        public async Task<double> GetAverageMarkForStudent(int studentId)
        {
            var studentIdparam = new SqlParameter("@studentId", studentId);
            var outParam = new SqlParameter
            {
                ParameterName = "@averageMark",
                SqlDbType = System.Data.SqlDbType.Float,
                Direction = System.Data.ParameterDirection.Output,
            };
            await Task.Run(() => dbContext.Database.ExecuteSqlRaw(StoredProcedures.GetAverageMarkForStudentProcedure + " @studentId, @averageMark OUT", studentIdparam, outParam));

            if (!(outParam.Value is double))
                throw new NullReferenceException(nameof(Marks));

            return (double)outParam.Value;
        }

        public async Task<double> GetAverageMarkInGroup(int groupId)
        {
            var groupIdparam = new SqlParameter("@groupId", groupId);
            var outParam = new SqlParameter
            {
                ParameterName = "@averageMark",
                SqlDbType = System.Data.SqlDbType.Float,
                Direction = System.Data.ParameterDirection.Output,
            };
            await Task.Run(() => dbContext.Database.ExecuteSqlRaw(StoredProcedures.GetAverageMarkInGroupProcedure + " @groupId, @averageMark OUT", groupIdparam, outParam));

            if (!(outParam.Value is double))
                throw new NullReferenceException(nameof(Marks));

            return (double)outParam.Value;
        }

        public async Task<Marks> GetByIdAsync(int id)
        {
            var model = await dbContext.Marks.FindAsync(id);

            if (model == null)
                throw new NullReferenceException(nameof(Marks));

            return model;
        }

        public IAsyncEnumerable<Marks> GetMarksForTimeByStudentId(int studentId, DateTime startDate) => dbContext.Marks
            .Include(x => x.Lesson)
            .AsNoTracking()
            .Where(x => x.MarkDate >= startDate && x.StudentId == studentId)
            .OrderByDescending(x => x.MarkDate)
            .AsAsyncEnumerable();

        public IAsyncEnumerable<Marks> GetMarksForTimeByLessonByStudentId(int studentId, int lessonId, DateTime startDate) => dbContext.Marks
            .Include(x => x.Lesson)
            .AsNoTracking()
            .Where(x => x.MarkDate >= startDate && x.StudentId == studentId && x.LessonId == lessonId)
            .OrderByDescending(x => x.MarkDate)
            .AsAsyncEnumerable();

        public IAsyncEnumerable<RatingByLesson> GetStudentRating(int studentId)
        {
            var studentIdparam = new SqlParameter("@studentId", studentId);
            return dbContext.Ratings.FromSqlRaw(StoredProcedures.GetStudentRatingForEachLesson + " @studentId", studentIdparam).AsNoTracking().AsAsyncEnumerable();
        }

        public async Task UpdateAsync(Marks model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(Marks));

            dbContext.Marks.Update(model);
            await dbContext.SaveChangesAsync();
        }

        public async Task<double> GetAverageMark() => await dbContext.Marks.AverageAsync(x => x.Mark);
    }
}
