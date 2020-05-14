using StudentPerfomance.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Interfaces
{
    public interface IMarkRepository : IRepository<Marks>
    {
        Task<double> GetAverageMarkByLessonForStudent(int studentId, int lessonId);

        Task<double> GetAverageMarkByLessonInGroup(int lessonId, int groupId);

        Task<double> GetAverageMarkForLesson(int lessonId);

        Task<double> GetAverageMarkForStudent(int studentId);

        Task<double> GetAverageMarkInGroup(int groupId);

        Task<double> GetAverageMark();

        IAsyncEnumerable<RatingByLesson> GetStudentRating(int studentId);

        IAsyncEnumerable<Marks> GetMarksForTimeByStudentId(int studentId, DateTime startDate);

        Task<IEnumerable<Marks>> GetTotalMarksForGroupByLessonId(int groupId, int lessonId, DateTime startDate, DateTime endDate);

        IAsyncEnumerable<Marks> GetMarksForTimeByLessonByStudentId(int studentId, int lseeonId, DateTime startDate);
    }
}
