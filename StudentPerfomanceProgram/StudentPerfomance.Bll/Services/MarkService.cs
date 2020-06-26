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
    public class MarkService : AbstractCrudService<Mark, MarkDto>, IMarkService
    {
        new private readonly IMarkRepository _repository;

        public MarkService(IMarkRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<double> GetAverageMarkByLessonForStudent(int studentId, int lessonId, DateTime startDate, DateTime endDate) => await _repository.GetAverageMarkByLessonForStudent(studentId, lessonId, startDate, endDate);

        public async Task<double> GetAverageMarkByLessonInGroup(int lessonId, int groupId, DateTime startDate, DateTime endDate) => await _repository.GetAverageMarkByLessonInGroup(lessonId, groupId, startDate, endDate);

        public async Task<double> GetAverageMarkForLesson(int lessonId, DateTime startDate, DateTime endDate) => await _repository.GetAverageMarkForLesson(lessonId, startDate, endDate);

        public async Task<double> GetAverageMarkForStudent(int studentId, DateTime startDate, DateTime endDate) => await _repository.GetAverageMarkForStudent(studentId, startDate, endDate);

        public async Task<double> GetAverageMarkInGroup(int groupId, DateTime startDate, DateTime endDate) => await _repository.GetAverageMarkInGroup(groupId, startDate, endDate);

        public async IAsyncEnumerable<MarkDto> GetMarksForTimeByStudentId(int studentId, DateTime startDate, DateTime endDate)
        {
            await foreach (var mark in _repository.GetMarksForTimeByStudentId(studentId, startDate, endDate))
                yield return mark.ToDto();
        }

        public async IAsyncEnumerable<MarkDto> GetMarksForTimeByLessonByStudentId(int studentId, int lessonId, DateTime startDate, DateTime endDate)
        {
            await foreach (var mark in _repository.GetMarksByLessonForStudent(studentId, lessonId, startDate, endDate))
                yield return mark.ToDto();
        }

        public async Task<RatingByLessonDto> GetBestLessonByMarkByStudentId(int studentId, DateTime startDate, DateTime endDate)
        {
            var marksStream = _repository.GetMarksForTimeByStudentId(studentId, startDate, endDate);
            var marks = new List<Mark>();

            await foreach (var mark in marksStream)
                marks.Add(mark);

            var bestLesson = new RatingByLessonDto();
            foreach (var group in marks.GroupBy(x => x.SubjectId))
            {
                var count = 0;
                var sum = 0.0;
                foreach (var item in group)
                {
                    count++;
                    sum += item.Grade ?? 0;
                }

                if (count != 0 && sum / count > bestLesson.Rating)
                {
                    bestLesson.Rating = Math.Round((sum / count), 2);
                    bestLesson.LessonId = group.Key;
                    //bestLesson.Title = group.FirstOrDefault()?.Subject?.Title;
                }
            }

            return bestLesson;
        }

        public async Task<RatingByLessonDto> GetWorstLessonByMarkByStudentId(int studentId, DateTime startDate, DateTime endDate)
        {
            var today = DateTime.Today;
            var marksStream = _repository.GetMarksForTimeByStudentId(studentId, startDate, endDate);
            var marks = new List<Mark>();

            await foreach (var mark in marksStream)
                marks.Add(mark);

            var worstLesson = new RatingByLessonDto();
            foreach (var group in marks.GroupBy(x => x.SubjectId))
            {
                var count = 0;
                var sum = 0.0;
                foreach (var item in group)
                {
                    count++;
                    sum += item.Grade ?? 0;
                }

                if (count != 0 && (sum / count < worstLesson.Rating || worstLesson.Rating == 0))
                {
                    worstLesson.Rating = Math.Round((sum / count), 2);
                    worstLesson.LessonId = group.Key;
                    //worstLesson.LessonId = group.FirstOrDefault()?.Subject?.Id;
                }
            }

            return worstLesson;
        }

        public async Task<double> GetProductivityForTimeByStudentId(int studentId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
            ////var marks = _repository.GetMarksForTimeByStudentId(studentId, startDate, endDate);

            ////var perviousMarks = new List<Mark>();
            ////var currentMarks = new List<Mark>();

            ////await foreach (var mark in marks)
            ////{
            ////    if (mark.Date >= DateTime.Today.AddDays(-term))
            ////        currentMarks.Add(mark);
            ////    else
            ////        perviousMarks.Add(mark);
            ////}

            ////if (!currentMarks.Any())
            ////    return 0;

            ////if (perviousMarks.Any() && perviousMarks.Average(x => x.Grade) != 0)
            ////    return (1 - (currentMarks.Average(x => x.Grade) / perviousMarks.Average(x => x.Grade))) * 100;

            ////return 100;
        }

        public async Task<double> GetProductivityForTimeByLessonByStudentId(int studentId, int lessonId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
            ////var marks = _repository.GetMarksByLessonForStudent(studentId, lessonId, DateTime.Today.AddDays(-term * 2));

            ////var perviousMarks = new List<Mark>();
            ////var currentMarks = new List<Mark>();

            ////await foreach (var mark in marks)
            ////{
            ////    if (mark.Date >= DateTime.Today.AddDays(-term))
            ////        currentMarks.Add(mark);
            ////    else
            ////        perviousMarks.Add(mark);
            ////}

            ////if (!currentMarks.Any())
            ////    return 0;

            ////if (perviousMarks.Any() && perviousMarks.Average(x => x.Grade) != 0)
            ////    return (1 - (currentMarks.Average(x => x.Grade) / perviousMarks.Average(x => x.Grade))) * 100;

            ////return 100;
        }

        public async Task<bool> GetGlobalRating(int studentId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
            //var studentAverageMark = await _repository.GetAverageMarkForStudent(studentId);
            //var globalAverageMark = await _repository.GetAverageMark();

            //return studentAverageMark > globalAverageMark;
        }

        public async Task<IEnumerable<MarkDto>> GetTotalMarksForGroupByLessonId(int groupId, int lessonId, DateTime startDate, DateTime endDate)
        {
            var marks = await _repository.FilterAsync(x => x.SubjectId == lessonId && x.Student.GroupId == groupId && x.Date >= startDate && x.Date <= endDate);

            return marks.GroupBy(x => x.StudentId).Select(x => new MarkDto { LessonId = lessonId, StudentId = x.Key, Mark = x.Sum(m => m.Grade), MarkDate = endDate });
        }

        public async Task<IEnumerable<RatingByLessonDto>> GetStudentRating(int studentId, DateTime startDate, DateTime endDate)
        {
            var marks = await _repository.FilterAsync(x => x.StudentId == studentId && x.Date >= startDate && x.Date <= endDate);

            return marks.GroupBy(x => x.SubjectId).Select(x => new RatingByLessonDto { LessonId = x.Key, Rating = x.Sum(y => y.Grade) });
        }

        public async Task<int> GetMissingsForStudent(int studnetId) => (await _repository.FilterAsync(x => x.StudentId == studnetId && x.Grade == null)).Count();
        public async Task<int> GetMissingsForStudentByLesson(int studnetId, int lessonId) => (await _repository.FilterAsync(x => x.StudentId == studnetId && x.SubjectId == lessonId && x.Grade == null)).Count();
    }
}
