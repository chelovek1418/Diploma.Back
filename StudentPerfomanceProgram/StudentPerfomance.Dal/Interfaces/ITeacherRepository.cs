using StudentPerfomance.Dal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Interfaces
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        Task<IEnumerable<Teacher>> GetUnconfirmedTeachers();
        Task ConfirmTeacher(int teacherId);
        Task AddLesson(int teacherId, int subjectId);
        Task DropLesson(int teacherId, int subjectId);
    }
}
