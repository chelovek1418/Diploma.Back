using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Bll.Extensions;
using StudentPerfomance.Bll.Interfaces;
using StudentPerfomance.Dal.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository _repository;

        public LessonService(ILessonRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(LessonDto model) => await _repository.CreateAsync(model.ToEntity());

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async IAsyncEnumerable<LessonDto> GetAllAsync()
        {
            var lessons = _repository.GetAllAsync();

            await foreach (var lesson in lessons)
            {
                yield return lesson.ToDto();
            }
        }

        public async Task<LessonDto> GetByIdAsync(int id) => (await _repository.GetByIdAsync(id)).ToDto();

        public async IAsyncEnumerable<LessonDto> GetLessonsByGroup(int groupId)
        {
            var lessons = _repository.GetLessonsByGroup(groupId);

            await foreach (var lesson in lessons)
            {
                yield return lesson.ToDto();
            }
        }

        public async Task UpdateAsync(LessonDto model) => await _repository.UpdateAsync(model.ToEntity());
    }
}
