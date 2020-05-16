using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Bll.Extensions;
using StudentPerfomance.Bll.Interfaces;
using StudentPerfomance.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _repository;

        public GroupService(IGroupRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CheckTitleAsync(string title) => await _repository.CheckTitleAsync(title);

        public async Task AddLesson(int groupId, int lessonId) => await _repository.AddLesson(groupId, lessonId);

        public async Task<int> CreateAsync(GroupDto model) => await _repository.CreateAsync(model.ToEntity());

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async Task DropLesson(int groupId, int lessonId) => await _repository.DropLesson(groupId, lessonId);

        public async IAsyncEnumerable<GroupDto> GetAllAsync()
        {
            await foreach (var group in _repository.GetAllAsync())
                yield return group.ToDto();
        }

        public async Task<GroupDto> GetByIdAsync(int id) => (await _repository.GetByIdAsync(id)).ToDto();

        public async Task UpdateAsync(GroupDto model) => await _repository.UpdateAsync(model.ToEntity());

        public async Task<IEnumerable<GroupDto>> GetByLessonAsync(int id) => (await _repository.FilterAsync(x => x.GroupSubjects.Any(y => y.SubjectId == id))).Select(x => x.ToDto());

        public async Task<IEnumerable<GroupDto>> SearchGroupsAsync(string term) => (await _repository.FilterAsync(x => x.Title.ToLower().Contains(term.ToLower()))).Select(x => x.ToDto());

        public async Task<GroupDto> GetWithMarksByLesson(int groupId, int lessonId, DateTime date) => (await _repository.GetWithMarksByLesson(groupId, lessonId, date)).ToDto();

        public async Task<int> GetCount() => await _repository.GetCount();
    }
}
