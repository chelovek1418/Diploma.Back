using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Bll.Interfaces;
using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Interfaces;

namespace StudentPerfomance.Bll.Services
{
    public class TeacherService : AbstractCrudService<Teacher, TeacherDto>, ITeacherService
    {
        public TeacherService(ITeacherRepository repository) : base(repository)
        {
        }
    }
}
