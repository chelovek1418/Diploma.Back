using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Bll.Extensions;
using StudentPerfomance.Bll.Interfaces;
using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Services
{
    public class DetailService : AbstractCrudService<Detail, DetailDto>, IDetailService
    {
        new private readonly IDetailRepository _repository;
        private readonly IGroupSubjectRepository _groupSubjectRepository;

        public DetailService(IDetailRepository repository, IGroupSubjectRepository groupSubjectRepository) : base(repository)
        {
            _repository = repository;
            _groupSubjectRepository = groupSubjectRepository;
        }

        public override async Task<int> CreateAsync(DetailDto model, Func<DetailDto, Detail> converter)
        {
            var groupSubjectId = (await _groupSubjectRepository.FilterAsync(x => x.GroupId == model.Group.Id && x.SubjectId == model.Subject.Id)).FirstOrDefault()?.Id;
            if(!groupSubjectId.HasValue)
                throw new NullReferenceException(nameof(GroupSubject));

            var entity = converter?.Invoke(model);
            entity.GroupSubjectId = groupSubjectId.Value;

            return await _repository.CreateAsync(entity);
        }

        public async Task<IEnumerable<DetailDto>> GetScheduleForGroup(int groupId, int semestr) => 
            (await _repository.GetScheduleForGroup(groupId, semestr)).Select(x => x.ToDto());

        public async Task<IEnumerable<DetailDto>> GetScheduleForTeacheByLessonAndGroup(int teacherId, int lessonId, int groupId, int semestr) =>
            (await _repository.GetScheduleForTeacheByLessonAndGroup(teacherId, lessonId, groupId, semestr)).Select(x => x.ToDto());

        public async Task<IEnumerable<DetailDto>> GetScheduleForTeacher(int teacherId, int semestr) => 
            (await _repository.GetScheduleForTeacher(teacherId, semestr)).Select(x => x.ToDto());
    }
}
