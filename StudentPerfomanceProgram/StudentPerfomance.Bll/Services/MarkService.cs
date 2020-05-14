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
    public class MarkService : IMarkService
    {
        private readonly IMarkRepository _repository;

        public MarkService(IMarkRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(MarkDto model) => await _repository.CreateAsync(model.ToEntity());

        public async Task DeleteAsync(int id) => await _repository.DeleteAsync(id);

        public async IAsyncEnumerable<MarkDto> GetAllAsync()
        {
            var marks = _repository.GetAllAsync();

            await foreach (var mark in marks)
            {
                yield return mark.ToDto();
            }
        }

        public async IAsyncEnumerable<RatingByLessonDto> GetStudentRating(int studentId)
        {
            var rating = _repository.GetStudentRating(studentId);

            await foreach (var position in rating)
            {
                yield return position.ToDto();
            }
        }

        public async Task<double> GetAverageMarkByLessonForStudent(int studentId, int lessonId) => await _repository.GetAverageMarkByLessonForStudent(studentId, lessonId);

        public async Task<double> GetAverageMarkByLessonInGroup(int lessonId, int groupId) => await _repository.GetAverageMarkByLessonInGroup(lessonId, groupId);

        public async Task<double> GetAverageMarkForLesson(int lessonId) => await _repository.GetAverageMarkForLesson(lessonId);

        public async Task<double> GetAverageMarkForStudent(int studentId) => await _repository.GetAverageMarkForStudent(studentId);

        public async Task<double> GetAverageMarkInGroup(int groupId) => await _repository.GetAverageMarkInGroup(groupId);

        public async Task<MarkDto> GetByIdAsync(int id) => (await _repository.GetByIdAsync(id)).ToDto();

        public async Task UpdateAsync(MarkDto model) => await _repository.UpdateAsync(model.ToEntity());

        public async IAsyncEnumerable<MarkDto> GetMarksForTimeByStudentId(int studentId, DateTime startDate)
        {
            var marks = _repository.GetMarksForTimeByStudentId(studentId, startDate);

            await foreach (var mark in marks)
            {
                yield return mark.ToDto();
            }
        }

        public async IAsyncEnumerable<MarkDto> GetMarksForTimeByLessonByStudentId(int studentId, int lessonId, DateTime startDate)
        {
            var marks = _repository.GetMarksForTimeByLessonByStudentId(studentId, lessonId, startDate);

            await foreach (var mark in marks)
            {
                yield return mark.ToDto();
            }
        }

        public async Task<RatingByLessonDto> GetBestLessonByMarkByStudentId(int studentId)
        {
            var today = DateTime.Today;
            var marksStream = _repository.GetMarksForTimeByStudentId(studentId, today.Month >= 9 ? new DateTime(today.Year, 9, 1) : new DateTime(today.Year, 1, 1));
            var marks = new List<Marks>();

            await foreach (var mark in marksStream)
                marks.Add(mark);

            var bestLesson = new RatingByLessonDto();
            foreach (var group in marks.GroupBy(x => x.LessonId))
            {
                var count = 0;
                var sum = 0.0;
                foreach (var item in group)
                {
                    count++;
                    sum += item.Mark;
                }

                if (count != 0 && sum / count > bestLesson.Rating)
                {
                    bestLesson.Rating = Math.Round((sum / count), 2);
                    bestLesson.Id = group.Key;
                    bestLesson.Title = group.FirstOrDefault()?.Lesson?.Title;
                }
            }

            return bestLesson;
        }

        public async Task<RatingByLessonDto> GetWorstLessonByMarkByStudentId(int studentId)
        {
            var today = DateTime.Today;
            var marksStream = _repository.GetMarksForTimeByStudentId(studentId, today.Month >= 9 ? new DateTime(today.Year, 9, 1) : new DateTime(today.Year, 1, 1));
            var marks = new List<Marks>();

            await foreach (var mark in marksStream)
                marks.Add(mark);

            var worstLesson = new RatingByLessonDto();
            foreach (var group in marks.GroupBy(x => x.LessonId))
            {
                var count = 0;
                var sum = 0.0;
                foreach (var item in group)
                {
                    count++;
                    sum += item.Mark;
                }

                if (count != 0 && (sum / count < worstLesson.Rating || worstLesson.Rating == 0))
                {
                    worstLesson.Rating = Math.Round((sum / count), 2);
                    worstLesson.Id = group.Key;
                    worstLesson.Title = group.FirstOrDefault()?.Lesson?.Title;
                }
            }

            return worstLesson;
        }

        public async Task<double> GetProductivityForTimeByStudentId(int studentId, int term)
        {
            var marks = _repository.GetMarksForTimeByStudentId(studentId, DateTime.Today.AddDays(-term * 2));

            var perviousMarks = new List<Marks>();
            var currentMarks = new List<Marks>();

            await foreach (var mark in marks)
            {
                if (mark.MarkDate >= DateTime.Today.AddDays(-term))
                    currentMarks.Add(mark);
                else
                    perviousMarks.Add(mark);
            }

            if (!currentMarks.Any())
                return 0;

            if (perviousMarks.Any() && perviousMarks.Average(x => x.Mark) != 0)
                return (1 - (currentMarks.Average(x => x.Mark) / perviousMarks.Average(x => x.Mark))) * 100;

            return 100;
        }

        public async Task<double> GetProductivityForTimeByLessonByStudentId(int studentId, int lessonId, int term)
        {
            var marks = _repository.GetMarksForTimeByLessonByStudentId(studentId, lessonId, DateTime.Today.AddDays(-term * 2));

            var perviousMarks = new List<Marks>();
            var currentMarks = new List<Marks>();

            await foreach (var mark in marks)
            {
                if (mark.MarkDate >= DateTime.Today.AddDays(-term))
                    currentMarks.Add(mark);
                else
                    perviousMarks.Add(mark);
            }

            if (!currentMarks.Any())
                return 0;

            if (perviousMarks.Any() && perviousMarks.Average(x => x.Mark) != 0)
                return (1 - (currentMarks.Average(x => x.Mark) / perviousMarks.Average(x => x.Mark))) * 100;

            return 100;
        }

        public async Task<bool> GetGlobalRating(int studentId)
        {
            var studentAverageMark = await _repository.GetAverageMarkForStudent(studentId);
            var globalAverageMark = await _repository.GetAverageMark();

            return studentAverageMark > globalAverageMark;
        }

        public async Task<IEnumerable<MarkDto>> GetTotalMarksForGroupByLessonId(int groupId, int lessonId, DateTime startDate, DateTime endDate) => 
            (await _repository.GetTotalMarksForGroupByLessonId(groupId, lessonId, startDate, endDate)).Select(x => x.ToDto());
    }
}
