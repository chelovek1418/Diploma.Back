using StudentPerfomance.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Interfaces
{
    public interface IMarkRepository : IRepository<Mark>
    {
        Task<double> GetAverageMarkByLessonForStudent(int studentId, int lessonId, DateTime startDate, DateTime endDate);

        Task<double> GetAverageMarkByLessonInGroup(int lessonId, int groupId, DateTime startDate, DateTime endDate);

        Task<double> GetAverageMarkForLesson(int lessonId, DateTime startDate, DateTime endDate);

        Task<double> GetAverageMarkForStudent(int studentId, DateTime startDate, DateTime endDate);

        Task<double> GetAverageMarkInGroup(int groupId, DateTime startDate, DateTime endDate);

        Task<double> GetAverageMark(DateTime startDate, DateTime endDate);

        IAsyncEnumerable<Mark> GetMarksForTimeByStudentId(int studentId, DateTime startDate, DateTime endDate);

        IAsyncEnumerable<Mark> GetMarksByLessonForStudent(int studentId, int lseeonId, DateTime startDate, DateTime endDate);
    }
}
