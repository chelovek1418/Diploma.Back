using StudentPerfomance.Bll.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Interfaces
{
    public interface IUserService : ICrudService<UserDto>
    {
        Task<int> CreateStudentAsync(StudentDto model);

        Task<StudentDto> GetStudentByIdAsync(int id);

        Task<StudentDto> GetBestStudent();

        Task<StudentDto> GetWorstStudent();

        Task<StudentDto> GetBestStudentForLessonInGroup(int lessonId, int groupId);

        Task<StudentDto> GetWorstStudentForLessonInGroup(int lessonId, int groupId);

        Task<StudentDto> GetBestStudentForLesson(int lessonId);

        Task<StudentDto> GetWorstStudentForLesson(int lessonId);

        Task<StudentDto> GetBestStudentInGroup(int groupId);

        Task<StudentDto> GetWorstStudentInGroup(int groupId);

        IAsyncEnumerable<StudentDto> GetTopStudents(DateTime date);
    }
}
