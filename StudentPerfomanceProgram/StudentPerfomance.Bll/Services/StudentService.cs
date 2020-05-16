using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Bll.Interfaces;
using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Interfaces;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Services
{
    public class StudentService : AbstractCrudService<Student, StudentDto>, IStudentService
    {
        new private readonly IStudentRepository _repository;
        public StudentService(IStudentRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public Task<StudentDto> GetBestStudent()
        {
            throw new System.NotImplementedException();
        }

        public Task<StudentDto> GetBestStudentForLesson(int lessonId)
        {
            throw new System.NotImplementedException();
        }

        public Task<StudentDto> GetBestStudentForLessonInGroup(int lessonId, int groupId)
        {
            throw new System.NotImplementedException();
        }

        public Task<StudentDto> GetBestStudentInGroup(int groupId)
        {
            throw new System.NotImplementedException();
        }

        public Task<StudentDto> GetStudentByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<StudentDto> GetWorstStudent()
        {
            throw new System.NotImplementedException();
        }

        public Task<StudentDto> GetWorstStudentForLesson(int lessonId)
        {
            throw new System.NotImplementedException();
        }

        public Task<StudentDto> GetWorstStudentForLessonInGroup(int lessonId, int groupId)
        {
            throw new System.NotImplementedException();
        }

        public Task<StudentDto> GetWorstStudentInGroup(int groupId)
        {
            throw new System.NotImplementedException();
        }
    }
}
