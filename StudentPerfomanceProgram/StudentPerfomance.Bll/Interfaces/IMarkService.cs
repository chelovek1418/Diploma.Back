using StudentPerfomance.Bll.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Interfaces
{
    public interface IMarkService : ICrudService<MarkDto>
    {
        Task<double> GetAverageMarkByLessonForStudent(int studentId, int lessonId);

        Task<double> GetAverageMarkByLessonInGroup(int lessonId, int groupId);

        Task<double> GetAverageMarkForLesson(int lessonId);

        Task<double> GetAverageMarkForStudent(int studentId);

        Task<double> GetAverageMarkInGroup(int groupId);

        IAsyncEnumerable<RatingByLessonDto> GetStudentRating(int studentId);

        Task<bool> GetGlobalRating(int studentId);

        IAsyncEnumerable<MarkDto> GetMarksForTimeByStudentId(int studentId, DateTime startDate);

        Task<RatingByLessonDto> GetBestLessonByMarkByStudentId(int studentId); 
        
        Task<RatingByLessonDto> GetWorstLessonByMarkByStudentId(int studentId);

        IAsyncEnumerable<MarkDto> GetMarksForTimeByLessonByStudentId(int studentId, int lseeonId, DateTime startDate);

        Task<double> GetProductivityForTimeByStudentId(int studentId, int term);

        Task<double> GetProductivityForTimeByLessonByStudentId(int studentId, int lessonId, int term);
    }
}
