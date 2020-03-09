using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StudentPerfomance.Dal.Constants.Procedures;

namespace StudentPerfomance.Dal.Repository
{
    public class StudentManagementRepository : ContextProvider
    {
        public StudentManagementRepository(StudentPerfomanceDbContext context) : base(context) { }

        public int? GetBestStudentId()
        {
            var param = new SqlParameter
            {
                ParameterName = "@bestStudentId",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output,
            };
            dbContext.Database.ExecuteSqlRaw(StoredProcedures.GetBestStudentId + " @bestStudentId OUT", param);
            dbContext.Marks.FromSqlRaw(StoredProcedures.GetBestStudentId);

            return param.Value as int?;
        }
    }
}
