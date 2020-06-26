using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Dal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Interfaces
{
    public interface ITeacherService : ICrudService<Teacher, TeacherDto>
    {
        Task<IEnumerable<TeacherDto>> GetUnconfirmedTeachers();
        Task<IEnumerable<TeacherDto>> SearchAsync(string search);
        Task<IEnumerable<TeacherDto>> GetByLesson(int lessonId);        
        Task ConfirmTeacher(int teacherId);
        Task AddLesson(int teacherId, int subjectId);
        Task DropLesson(int teacherId, int subjectId);
    }
}
