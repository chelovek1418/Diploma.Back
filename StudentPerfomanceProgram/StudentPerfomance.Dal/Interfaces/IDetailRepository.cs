using StudentPerfomance.Dal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Interfaces
{
    public interface IDetailRepository : IRepository<Detail>
    {
        Task<IEnumerable<Detail>> GetScheduleForGroup(int groupId, int semestr);

        Task<IEnumerable<Detail>> GetScheduleForTeacher(int teacherId, int semestr);
    }
}
