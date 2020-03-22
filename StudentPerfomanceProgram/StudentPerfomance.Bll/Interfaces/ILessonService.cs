using StudentPerfomance.Bll.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Interfaces
{
    public interface ILessonService : ICrudService<LessonDto>
    {
        IAsyncEnumerable<LessonDto> GetLessonsByGroup(int groupId);
        //Task<LessonDto> GetBestLesson(int studentId);
        //Task<LessonDto> GetWorstLesson(int studentId);
    }
}
