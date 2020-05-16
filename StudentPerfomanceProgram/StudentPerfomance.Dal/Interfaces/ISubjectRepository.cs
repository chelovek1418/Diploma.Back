using StudentPerfomance.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Interfaces
{
    public interface ISubjectRepository : IRepository<Subject>
    {
        Task<bool> CheckTitleAsync(string title);

        Task<IEnumerable<Subject>> GetLessonsWithMarksForTimeByStudentId(int studentId, DateTime startDate, DateTime endDate);
    }
}
