using StudentPerfomance.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<int> CreateStudentAsync(Students model);

        Task UpdateStudentAsync(Students model);

        Task<Students> GetStudentByIdAsync(int id);

        Task<Students> GetBestStudent();

        Task<Students> GetWorstStudent();

        Task<Students> GetBestStudentForLessonInGroup(int lessonId, int groupId);

        Task<Students> GetWorstStudentForLessonInGroup(int lessonId, int groupId);

        Task<Students> GetBestStudentForLesson(int lessonId);

        Task<Students> GetWorstStudentForLesson(int lessonId);

        Task<Students> GetBestStudentInGroup(int groupId);

        Task<Students> GetWorstStudentInGroup(int groupId);

        IAsyncEnumerable<Students> GetTopStudents(DateTime date);

        IAsyncEnumerable<Students> GetStudents(int take, int skip);

        IAsyncEnumerable<User> SearchAsync(string term);

        IAsyncEnumerable<Students> SearchStudentsAsync(string term);
    }
}
