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
    public class LessonService : ILessonService
    {
        private readonly ISubjectRepository _repository;

        public LessonService(ISubjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CheckTitleAsync(string title) => await _repository.CheckTitleAsync(title);

        public async Task<int> CreateAsync(LessonDto model) => await _repository.CreateAsync(model.ToEntity());

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async IAsyncEnumerable<LessonDto> GetAllAsync()
        {
            await foreach (var lesson in _repository.GetAllAsync())
                yield return lesson.ToDto();
        }

        public async Task<LessonDto> GetByIdAsync(int id) => (await _repository.GetByIdAsync(id)).ToDto();

        public async Task<int> GetCount() => await _repository.GetCount();

        public async Task<IEnumerable<LessonDto>> GetLessonsByGroup(int groupId) => (await _repository.FilterAsync(x => x.GroupSubjects.Any(g => g.GroupId == groupId))).Select(x => x.ToDto());

        public async Task<IEnumerable<LessonDto>> GetLessonsWithMarksForTimeByStudentId(int studentId, DateTime startDate, DateTime endDate) =>
            (await _repository.GetLessonsWithMarksForTimeByStudentId(studentId, startDate, endDate)).Select(x => x.ToDto());

        public async Task<IEnumerable<LessonDto>> SearchLessons(string term) => (await _repository.FilterAsync(x => x.Title.ToLower().Contains(term.ToLower()))).Select(x => x.ToDto());

        public async Task UpdateAsync(LessonDto model) => await _repository.UpdateAsync(model.ToEntity());
    }
}
