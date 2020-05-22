using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Dal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Interfaces
{
    public interface IDetailService : ICrudService<Detail, DetailDto>
    {
        Task<IEnumerable<DetailDto>> GetScheduleForGroup(int groupId, int semestr);
        Task<IEnumerable<DetailDto>> GetScheduleForTeacher(int teacherId, int semestr);
    }
}
