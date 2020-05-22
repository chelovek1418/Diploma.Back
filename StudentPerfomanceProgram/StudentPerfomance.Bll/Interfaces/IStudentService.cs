using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Interfaces
{
    public interface IStudentService : ICrudService<Student, StudentDto>
    {
        Task<IEnumerable<StudentDto>> FilterAsync(string term);
        Task<StudentDto> GetStudentByIdAsync(int id);

        Task<StudentDto> GetBestStudent();

        Task<StudentDto> GetWorstStudent();

        Task<StudentDto> GetBestStudentForLessonInGroup(int lessonId, int groupId);

        Task<StudentDto> GetWorstStudentForLessonInGroup(int lessonId, int groupId);

        Task<StudentDto> GetBestStudentForLesson(int lessonId);

        Task<StudentDto> GetWorstStudentForLesson(int lessonId);

        Task<StudentDto> GetBestStudentInGroup(int groupId);

        Task<StudentDto> GetWorstStudentInGroup(int groupId);

        Task<IEnumerable<StudentDto>> GetTopStudents(DateTime startDate, DateTime endDate);
    }
}
