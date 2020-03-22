using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentPerfomance.Dal.Constants;
using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Repository
{
    public class UserRepository : ContextProvider, IUserRepository
    {
        public UserRepository(StudentPerfomanceDbContext context) : base(context)
        {
        }

        public async Task<int> CreateAsync(User model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(User));

            await dbContext.User.AddAsync(model);
            await dbContext.SaveChangesAsync();

            return model.Id;
        }

        public async Task<int> CreateStudentAsync(Students model)
        {
            if (model == null || model.IdNavigation == null)
                throw new ArgumentNullException(nameof(Students));

            await dbContext.Students.AddAsync(model);
            await dbContext.SaveChangesAsync();

            return model.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var model = await dbContext.User.FindAsync(id);

            if (model == null)
                throw new NullReferenceException(nameof(User));

            dbContext.User.Remove(model);
            await dbContext.SaveChangesAsync();
        }

        public IAsyncEnumerable<User> GetAllAsync() => dbContext.User.AsAsyncEnumerable();

        public async Task<User> GetByIdAsync(int id)
        {
            var model = await dbContext.User.FindAsync(id);

            if (model == null)
                throw new NullReferenceException(nameof(User));

            return model;
        }

        public async Task<Students> GetStudentByIdAsync(int id)
        {
            var model = await dbContext.Students.Include(x => x.IdNavigation).FirstOrDefaultAsync(u => u.Id == id);

            if (model == null || model.IdNavigation == null)
                throw new NullReferenceException(nameof(Students));

            return model;
        }

        public async Task UpdateAsync(User model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(User));

            dbContext.User.Update(model);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Students> GetBestStudent()
        {
            var bestStudentId = GetBestStudentId();

            if (!bestStudentId.HasValue)
                throw new ArgumentNullException(nameof(Students));

            return await GetStudentByIdAsync(bestStudentId.Value);
        }

        public async Task<Students> GetBestStudentForLessonInGroup(int lessonId, int groupId)
        {
            var bestStudentId = GetBestStudentIdForLessonInGroup(lessonId, groupId);

            if (!bestStudentId.HasValue)
                throw new ArgumentNullException(nameof(Students));

            return await GetStudentByIdAsync(bestStudentId.Value);
        }

        public async Task<Students> GetWorstStudentForLessonInGroup(int lessonId, int groupId)
        {
            var bestStudentId = GetWorstStudentIdForLessonInGroup(lessonId, groupId);

            if (!bestStudentId.HasValue)
                throw new ArgumentNullException(nameof(Students));

            return await GetStudentByIdAsync(bestStudentId.Value);
        }

        public async Task<Students> GetBestStudentForLesson(int lessonId)
        {
            var bestStudentId = GetBestStudentIdForLesson(lessonId);

            if (!bestStudentId.HasValue)
                throw new ArgumentNullException(nameof(Students));

            return await GetStudentByIdAsync(bestStudentId.Value);
        }

        public async Task<Students> GetWorstStudentForLesson(int lessonId)
        {
            var worstStudentId = GetWorstStudentIdForLesson(lessonId);

            if (!worstStudentId.HasValue)
                throw new ArgumentNullException(nameof(Students));

            return await GetStudentByIdAsync(worstStudentId.Value);
        }

        public async Task<Students> GetBestStudentInGroup(int groupId)
        {
            var bestStudentId = GetBestStudentIdInGroup(groupId);

            if (!bestStudentId.HasValue)
                throw new ArgumentNullException(nameof(Students));

            return await GetStudentByIdAsync(bestStudentId.Value);
        }

        public async Task<Students> GetWorstStudentInGroup(int groupId)
        {
            var worstStudentId = GetWorstStudentIdInGroup(groupId);

            if (!worstStudentId.HasValue)
                throw new ArgumentNullException(nameof(Students));

            return await GetStudentByIdAsync(worstStudentId.Value);
        }

        public async Task<Students> GetWorstStudent()
        {
            var bestStudentId = GetWorstStudentId();

            if (!bestStudentId.HasValue)
                throw new ArgumentNullException(nameof(Students));

            return await GetStudentByIdAsync(bestStudentId.Value);
        }

        public async IAsyncEnumerable<Students> GetTopStudents(DateTime date)
        {
            var dateParam = new SqlParameter("@startDate", date.ToString("yyyy-MM-dd"));
            foreach (var student in dbContext.Students.FromSqlRaw(StoredProcedures.GetTopStudents + " @startDate", dateParam).AsNoTracking().ToList())
                yield return await dbContext.Students.Include(x => x.IdNavigation).FirstOrDefaultAsync(x => x == student);
        }

            #region Privates

        private int? GetBestStudentIdInGroup(int groupId)
        {
            var groupIdparam = new SqlParameter("@groupId", groupId);
            var outParam = new SqlParameter
            {
                ParameterName = "@bestStudentId",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output,
            };
            dbContext.Database.ExecuteSqlRaw(StoredProcedures.GetBestStudentIdInGroupProcedure + " @groupId, @bestStudentId OUT", groupIdparam, outParam);

            return outParam.Value as int?;
        }

        private int? GetWorstStudentIdInGroup(int groupId)
        {
            var groupIdparam = new SqlParameter("@groupId", groupId);
            var outParam = new SqlParameter
            {
                ParameterName = "@worstStudentId",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output,
            };
            dbContext.Database.ExecuteSqlRaw(StoredProcedures.GetWorstStudentIdInGroupProcedure + " @groupId, @worstStudentId OUT", groupIdparam, outParam);

            return outParam.Value as int?;
        }

        private int? GetBestStudentIdForLesson(int lessonId)
        {
            var lessonIdparam = new SqlParameter("@lessonId", lessonId);
            var outParam = new SqlParameter
            {
                ParameterName = "@bestStudentId",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output,
            };
            dbContext.Database.ExecuteSqlRaw(StoredProcedures.GetBestStudentIdForLessonProcedure + " @lessonId, @bestStudentId OUT", lessonIdparam, outParam);

            return outParam.Value as int?;
        }

        private int? GetWorstStudentIdForLesson(int lessonId)
        {
            var lessonIdparam = new SqlParameter("@lessonId", lessonId);
            var outParam = new SqlParameter
            {
                ParameterName = "@worstStudentId",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output,
            };
            dbContext.Database.ExecuteSqlRaw(StoredProcedures.GetWorstStudentIdForLessonProcedure + " @lessonId, @worstStudentId OUT", lessonIdparam, outParam);

            return outParam.Value as int?;
        }

        private int? GetBestStudentIdForLessonInGroup(int lessonId, int groupId)
        {
            var lessonIdparam = new SqlParameter("@lessonId", lessonId);
            var groupIdparam = new SqlParameter("@groupId", groupId);
            var outParam = new SqlParameter
            {
                ParameterName = "@bestStudentId",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output,
            };
            dbContext.Database.ExecuteSqlRaw(StoredProcedures.GetBestStudentIdForLessonInGroupProcedure + " @lessonId, @groupId, @bestStudentId OUT", lessonIdparam, groupIdparam, outParam);

            return outParam.Value as int?;
        }

        private int? GetWorstStudentIdForLessonInGroup(int lessonId, int groupId)
        {
            var lessonIdparam = new SqlParameter("@lessonId", lessonId);
            var groupIdparam = new SqlParameter("@groupId", groupId);
            var outParam = new SqlParameter
            {
                ParameterName = "@worstStudentId",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output,
            };
            dbContext.Database.ExecuteSqlRaw(StoredProcedures.GetWorstStudentIdForLessonInGroupProcedure + " @lessonId, @groupId, @worstStudentId OUT", lessonIdparam, groupIdparam, outParam);

            return outParam.Value as int?;
        }

        private int? GetBestStudentId()
        {
            var param = new SqlParameter
            {
                ParameterName = "@bestStudentId",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output,
            };
            dbContext.Database.ExecuteSqlRaw(StoredProcedures.GetBestStudentId + " @bestStudentId OUT", param);

            return param.Value as int?;
        }

        private int? GetWorstStudentId()
        {
            var param = new SqlParameter
            {
                ParameterName = "@bestStudentId",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output,
            };
            dbContext.Database.ExecuteSqlRaw(StoredProcedures.GetWorstStudentId + " @bestStudentId OUT", param);

            return param.Value as int?;
        }

        #endregion
    }
}
