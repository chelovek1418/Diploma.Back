using StudentPerfomance.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Interfaces
{
    public interface IGroupRepository : IRepository<Groups>
    {
        Task AddLesson(int groupId, int lessonId);
        Task DropLesson(int groupId, int lessonId);
        Task<bool> CheckTitleAsync(string title);
        Task<Groups> GetWithMarksByLesson(int groupId, int lessonId, DateTime date);
        IAsyncEnumerable<Groups> GetByLessonAsync(int id);
        IAsyncEnumerable<Groups> SearchAsync(string term);
    }
}
