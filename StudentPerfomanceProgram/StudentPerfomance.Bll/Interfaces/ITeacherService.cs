using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Dal.Entities;

namespace StudentPerfomance.Bll.Interfaces
{
    public interface ITeacherService : ICrudService<Teacher, TeacherDto>
    {
    }
}
