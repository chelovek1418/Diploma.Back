using StudentPerfomance.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Interfaces
{
    public interface ILessonRepository : IRepository<Lessons>
    {
        IAsyncEnumerable<Lessons> GetLessonsByGroup(int groupId);

        Task<bool> CheckTitleAsync(string title);

        Task<IEnumerable<Lessons>> GetLessonsWithMarksForTimeByStudentId(int studentId, DateTime startDate, DateTime endDate);

        IAsyncEnumerable<Lessons> SearchAsync(string term);
    }
}
