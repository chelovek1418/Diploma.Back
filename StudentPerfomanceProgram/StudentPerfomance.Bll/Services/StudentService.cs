using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Bll.Extensions;
using StudentPerfomance.Bll.Interfaces;
using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<StudentDto>> FilterAsync(string term) => 
            (await _repository.FilterAsync(x => x.User.Email.ToLower().StartsWith(term) || x.User.FirstName.ToLower().StartsWith(term) || x.User.LastName.ToLower().StartsWith(term))).Select(x => x.ToDto());

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

        public async Task<IEnumerable<StudentDto>> GetTopStudents(DateTime startDate, DateTime endDate) => 
            (await _repository.GetTopStudents(startDate, endDate)).Select(x => x.ToDto());

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
