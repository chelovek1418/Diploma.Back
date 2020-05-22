using StudentPerfomance.Dal.Entities;
using System;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Interfaces
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task AddLesson(int groupId, int lessonId);
        Task DropLesson(int groupId, int lessonId);
        Task<bool> CheckTitleAsync(string title);
        Task<Group> GetWithMarksByLesson(int groupId, int lessonId, DateTime date);
    }
}
