using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Interfaces
{
    public interface IMarkService : ICrudService<Mark, MarkDto>
    {
        Task<double> GetAverageMarkByLessonForStudent(int studentId, int lessonId, DateTime startDate, DateTime endDate);

        Task<double> GetAverageMarkByLessonInGroup(int lessonId, int groupId, DateTime startDate, DateTime endDate);

        Task<double> GetAverageMarkForLesson(int lessonId, DateTime startDate, DateTime endDate);

        Task<double> GetAverageMarkForStudent(int studentId, DateTime startDate, DateTime endDate);

        Task<double> GetAverageMarkInGroup(int groupId, DateTime startDate, DateTime endDate);

        Task<IEnumerable<RatingByLessonDto>> GetStudentRating(int studentId, DateTime startDate, DateTime endDate);

        Task<bool> GetGlobalRating(int studentId, DateTime startDate, DateTime endDate);

        IAsyncEnumerable<MarkDto> GetMarksForTimeByStudentId(int studentId, DateTime startDate, DateTime endDate);

        Task<RatingByLessonDto> GetBestLessonByMarkByStudentId(int studentId, DateTime startDate, DateTime endDate); 
        
        Task<RatingByLessonDto> GetWorstLessonByMarkByStudentId(int studentId, DateTime startDate, DateTime endDate);

        IAsyncEnumerable<MarkDto> GetMarksForTimeByLessonByStudentId(int studentId, int lessonId, DateTime startDate, DateTime endDate);

        Task<double> GetProductivityForTimeByStudentId(int studentId, DateTime startDate, DateTime endDate);

        Task<double> GetProductivityForTimeByLessonByStudentId(int studentId, int lessonId, DateTime startDate, DateTime endDate);

        Task<IEnumerable<MarkDto>> GetTotalMarksForGroupByLessonId(int groupId, int lessonId, DateTime startDate, DateTime endDate);
    }
}
