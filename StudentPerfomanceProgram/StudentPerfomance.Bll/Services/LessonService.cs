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
    public class LessonService : AbstractCrudService<Subject, LessonDto>, ILessonService
    {
        new private readonly ISubjectRepository _repository;

        public LessonService(ISubjectRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<bool> CheckTitleAsync(string title) => !(await _repository.FilterAsync(x => x.Title == title)).Any();

        public async Task<IEnumerable<LessonDto>> GetLessonsByGroup(int groupId) => (await _repository.FilterAsync(x => x.GroupSubjects.Any(g => g.GroupId == groupId))).Select(x => x.ToDto());

        public async Task<IEnumerable<LessonDto>> GetLessonsWithMarksForTimeByStudentId(int studentId, DateTime startDate, DateTime endDate) =>
            (await _repository.GetLessonsWithMarksForTimeByStudentId(studentId, startDate, endDate)).Select(x => x.ToDto());

        public async Task<IEnumerable<LessonDto>> SearchLessons(string term) => (await _repository.FilterAsync(x => x.Title.ToLower().Contains(term.ToLower()))).Select(x => x.ToDto());
    }
}
