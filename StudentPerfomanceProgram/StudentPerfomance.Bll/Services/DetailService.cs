using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Bll.Extensions;
using StudentPerfomance.Bll.Interfaces;
using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Services
{
    public class DetailService : AbstractCrudService<Detail, DetailDto>, IDetailService
    {
        new private readonly IDetailRepository _repository;
        public DetailService(IDetailRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DetailDto>> GetScheduleForGroup(int groupId, int semestr) => 
            (await _repository.GetScheduleForGroup(groupId, semestr)).Select(x => x.ToDto());

        public async Task<IEnumerable<DetailDto>> GetScheduleForTeacher(int teacherId, int semestr) => 
            (await _repository.GetScheduleForTeacher(teacherId, semestr)).Select(x => x.ToDto());
    }
}
