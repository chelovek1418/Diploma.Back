using StudentPerfomance.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Interfaces
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student> GetBestStudent(DateTime startDate, DateTime endDate);
        Task<Student> GetWorstStudent(DateTime startDate, DateTime endDate);
        Task<Student> GetBestStudentForLessonInGroup(int lessonId, int groupId, DateTime startDate, DateTime endDate);
        Task<Student> GetWorstStudentForLessonInGroup(int lessonId, int groupId, DateTime startDate, DateTime endDate);
        Task<Student> GetBestStudentForLesson(int lessonId, DateTime startDate, DateTime endDate);
        Task<Student> GetWorstStudentForLesson(int lessonId, DateTime startDate, DateTime endDate);
        Task<Student> GetWorstStudentInGroup(int groupId, DateTime startDate, DateTime endDate);
        Task<Student> GetBestStudentInGroup(int groupId, DateTime startDate, DateTime endDate);
        IAsyncEnumerable<Student> GetTopStudents(DateTime startDate, DateTime endDate);
    }
}
