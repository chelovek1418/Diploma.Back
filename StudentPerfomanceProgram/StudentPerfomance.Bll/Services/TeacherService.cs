using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Bll.Extensions;
using StudentPerfomance.Bll.Interfaces;
using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Services
{
    public class TeacherService : AbstractCrudService<Teacher, TeacherDto>, ITeacherService
    {
        new private readonly ITeacherRepository _repository;
        public TeacherService(ITeacherRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TeacherDto>> GetUnconfirmedTeachers() => (await _repository.GetUnconfirmedTeachers()).Select(x => x.ToDto());

        public async Task ConfirmTeacher(int teacherId) => await _repository.ConfirmTeacher(teacherId);

        public async Task AddLesson(int teacherId, int subjectId) => await _repository.AddLesson(teacherId, subjectId);

        public async Task DeopLesson(int teacherId, int subjectId) => await _repository.DropLesson(teacherId, subjectId);
    }
}
