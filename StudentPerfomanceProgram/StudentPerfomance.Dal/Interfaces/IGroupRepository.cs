using StudentPerfomance.Dal.Entities;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Interfaces
{
    public interface IGroupRepository : IRepository<Groups>
    {
        Task AddLesson(int groupId, int lessonId);
        Task DropLesson(int groupId, int lessonId);
    }
}
