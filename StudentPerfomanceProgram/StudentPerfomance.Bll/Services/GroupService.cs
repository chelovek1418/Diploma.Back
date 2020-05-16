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
    public class GroupService : AbstractCrudService<Group, GroupDto>, IGroupService
    {
        new private readonly IGroupRepository _repository;
        public GroupService(IGroupRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<bool> CheckTitleAsync(string title) => await _repository.CheckTitleAsync(title);

        public async Task AddLesson(int groupId, int lessonId) => await _repository.AddLesson(groupId, lessonId);

        public async Task DropLesson(int groupId, int lessonId) => await _repository.DropLesson(groupId, lessonId);

        public async Task<IEnumerable<GroupDto>> GetByLessonAsync(int id) => (await _repository.FilterAsync(x => x.GroupSubjects.Any(y => y.SubjectId == id))).Select(x => x.ToDto());

        public async Task<IEnumerable<GroupDto>> SearchGroupsAsync(string term) => (await _repository.FilterAsync(x => x.Title.ToLower().Contains(term.ToLower()))).Select(x => x.ToDto());

        public async Task<GroupDto> GetWithMarksByLesson(int groupId, int lessonId, DateTime date) => (await _repository.GetWithMarksByLesson(groupId, lessonId, date)).ToDto();
    }
}
