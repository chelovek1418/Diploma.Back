using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Dal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Interfaces
{
    public interface ITeacherService : ICrudService<Teacher, TeacherDto>
    {
        public Task<IEnumerable<TeacherDto>> GetUnconfirmedTeachers();
        Task ConfirmTeacher(int teacherId);
        Task AddLesson(int teacherId, int subjectId);
        Task DeopLesson(int teacherId, int subjectId);
    }
}
