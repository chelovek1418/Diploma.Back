using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Bll.Extensions;
using StudentPerfomance.Bll.Interfaces;
using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Services
{
    public class LessonService : ICrudService<LessonDto>
    {
        private readonly IRepository<Lessons> _repository;

        public LessonService(IRepository<Lessons> repository)
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

        public async Task UpdateAsync(LessonDto model) => await _repository.UpdateAsync(model.ToEntity());
    }
}
