using StudentPerfomance.Bll.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Interfaces
{
    public interface ILessonService : ICrudService<LessonDto>
    {
        Task<IEnumerable<LessonDto>> GetLessonsByGroup(int groupId);
        Task<bool> CheckTitleAsync(string title);
        Task<IEnumerable<LessonDto>> GetLessonsWithMarksForTimeByStudentId(int studentId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<LessonDto>> SearchLessons(string term);
        //Task<LessonDto> GetBestLesson(int studentId);
        //Task<LessonDto> GetWorstLesson(int studentId);
    }
}
