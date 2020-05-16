using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Bll.Extensions;
using StudentPerfomance.Bll.Interfaces;
using StudentPerfomance.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public Task<int> CreateAsync(UserDto model)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateStudentAsync(StudentDto model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<UserDto> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StudentDto> GetBestStudent()
        {
            throw new NotImplementedException();
        }

        public Task<StudentDto> GetBestStudentForLesson(int lessonId)
        {
            throw new NotImplementedException();
        }

        public Task<StudentDto> GetBestStudentForLessonInGroup(int lessonId, int groupId)
        {
            throw new NotImplementedException();
        }

        public Task<StudentDto> GetBestStudentInGroup(int groupId)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCount()
        {
            throw new NotImplementedException();
        }

        public Task<StudentDto> GetStudentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<StudentDto> GetStudents(int take, int skip)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<StudentDto> GetTopStudents(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<StudentDto> GetWorstStudent()
        {
            throw new NotImplementedException();
        }

        public Task<StudentDto> GetWorstStudentForLesson(int lessonId)
        {
            throw new NotImplementedException();
        }

        public Task<StudentDto> GetWorstStudentForLessonInGroup(int lessonId, int groupId)
        {
            throw new NotImplementedException();
        }

        public Task<StudentDto> GetWorstStudentInGroup(int groupId)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<UserDto> SearchAsync(string term)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<StudentDto> SearchStudentsAsync(string term)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UserDto model)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStudentAsync(StudentDto model)
        {
            throw new NotImplementedException();
        }
    }
}
