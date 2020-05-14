using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Bll.Extensions;
using StudentPerfomance.Bll.Interfaces;
using StudentPerfomance.Dal.Interfaces;
using System;
using System.Collections.Generic;
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
            var groups = _repository.GetAllAsync();

            await foreach (var group in groups)
            {
                yield return group.ToDto();
            }
        }

        public async Task<GroupDto> GetByIdAsync(int id) => (await _repository.GetByIdAsync(id)).ToDto();

        public async Task UpdateAsync(GroupDto model) => await _repository.UpdateAsync(model.ToEntity());

        public async IAsyncEnumerable<GroupDto> GetByLessonAsync(int id)
        {
            await foreach (var group in _repository.GetByLessonAsync(id))
                yield return group.ToDto();
        }

        public async IAsyncEnumerable<GroupDto> SearchGroupsAsync(string term)
        {
            await foreach (var group in _repository.SearchAsync(term))
                yield return group.ToDto();
        }

        public async Task<GroupDto> GetWithMarksByLesson(int groupId, int lessonId, DateTime date) => (await _repository.GetWithMarksByLesson(groupId, lessonId, date)).ToDto();
    }
}
