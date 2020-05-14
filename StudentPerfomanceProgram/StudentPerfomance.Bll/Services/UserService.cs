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

        public async Task<int> CreateAsync(UserDto model) => await _repository.CreateAsync(model.ToEntity());

        public async Task<int> CreateStudentAsync(StudentDto model) => await _repository.CreateStudentAsync(model.ToEntity());

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async IAsyncEnumerable<UserDto> GetAllAsync()
        {
            await foreach (var user in _repository.GetAllAsync())
                yield return user.ToDto();
        }

        public async IAsyncEnumerable<UserDto> SearchAsync(string term)
        {
            await foreach (var user in _repository.SearchAsync(term))
                yield return user.ToDto();
        }

        public async IAsyncEnumerable<StudentDto> SearchStudentsAsync(string term)
        {
            await foreach (var student in _repository.SearchStudentsAsync(term))
                yield return student.ToDto();
        }

        public async Task<StudentDto> GetBestStudent() => (await _repository.GetBestStudent()).ToDto();

        public async Task<StudentDto> GetWorstStudent() => (await _repository.GetWorstStudent()).ToDto();

        public async Task<StudentDto> GetBestStudentForLesson(int lessonId) => (await _repository.GetBestStudentForLesson(lessonId)).ToDto();

        public async Task<StudentDto> GetWorstStudentForLesson(int lessonId) => (await _repository.GetWorstStudentForLesson(lessonId)).ToDto();

        public async Task<StudentDto> GetBestStudentForLessonInGroup(int lessonId, int groupId) => (await _repository.GetBestStudentForLessonInGroup(lessonId, groupId)).ToDto();

        public async Task<StudentDto> GetWorstStudentForLessonInGroup(int lessonId, int groupId) => (await _repository.GetWorstStudentForLessonInGroup(lessonId, groupId)).ToDto();

        public async Task<StudentDto> GetBestStudentInGroup(int groupId) => (await _repository.GetBestStudentInGroup(groupId)).ToDto();

        public async Task<StudentDto> GetWorstStudentInGroup(int groupId) => (await _repository.GetWorstStudentInGroup(groupId)).ToDto();

        public async Task<UserDto> GetByIdAsync(int id) => (await _repository.GetByIdAsync(id)).ToDto();

        public async Task<StudentDto> GetStudentByIdAsync(int id) => (await _repository.GetStudentByIdAsync(id)).ToDto();

        public async Task UpdateAsync(UserDto model) => await _repository.UpdateAsync(model.ToEntity());

        public async IAsyncEnumerable<StudentDto> GetTopStudents(DateTime date)
        {
            await foreach (var student in _repository.GetTopStudents(date))
                yield return student.ToDto();
        }

        public async IAsyncEnumerable<StudentDto> GetStudents(int take, int skip)
        {
            await foreach (var student in _repository.GetStudents(take, skip))
                yield return student.ToDto();
        }

        public async Task UpdateStudentAsync(StudentDto model) => await _repository.UpdateStudentAsync(model.ToEntity());
    }
}
