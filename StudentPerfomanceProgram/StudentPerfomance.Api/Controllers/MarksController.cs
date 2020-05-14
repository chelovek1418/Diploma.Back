using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.Api.Extensions;
using StudentPerfomance.Api.Helpers;
using StudentPerfomance.Api.ViewModels.MarkViewModels;
using StudentPerfomance.Bll.Interfaces;

namespace StudentPerfomance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarksController : ControllerBase
    {
        private readonly IMarkService _markService;

        public MarksController(IMarkService markService)
        {
            _markService = markService;
        }

        [HttpGet]
        public async IAsyncEnumerable<MarkViewModel> Get()
        {
            var marks = _markService.GetAllAsync();

            await foreach (var mark in marks)
            {
                yield return mark.ToViewModel();
            }
        }

        [HttpGet("[action]")]
        public async IAsyncEnumerable<RatingByLessonViewModel> GetRating(int studentId)
        {
            var rating = _markService.GetStudentRating(studentId);

            await foreach (var position in rating)
            {
                yield return position.ToViewModel();
            }
        }

        [HttpGet("[action]")]
        public async IAsyncEnumerable<MarkViewModel> GetMarksForTime(int studentId, DateTime? date)
        {
            if (date == null || date.Value > DateTime.Now || date.Value < DateTime.Today.AddYears(-10))
                date = DateTimeHelper.GetTermStartDate();

            var marks = _markService.GetMarksForTimeByStudentId(studentId, (DateTime)date);

            await foreach (var mark in marks)
            {
                yield return mark.ToViewModel();
            }
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<MarkViewModel>> GetTotalMarksForGroupByLesson(int groupId, int lessonId, DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || startDate.Value > DateTime.Now || startDate.Value < DateTime.Today.AddYears(-10))
                startDate = DateTimeHelper.GetTermStartDate();

            if (endDate == null || endDate.Value <= startDate.Value || endDate.Value > DateTime.Today)
                endDate = DateTime.Now;

            return (await _markService.GetTotalMarksForGroupByLessonId(groupId, lessonId, startDate.Value, endDate.Value)).Select(x => x.ToViewModel());
        }

        [HttpGet("[action]")]
        public async IAsyncEnumerable<MarkViewModel> GetMarksForTimeByLesson(int studentId, int lessonId, DateTime? date)
        {
            if (date == null || date.Value > DateTime.Now || date.Value < DateTime.Today.AddYears(-10))
                date = DateTimeHelper.GetTermStartDate();

            var marks = _markService.GetMarksForTimeByLessonByStudentId(studentId, lessonId, (DateTime)date);

            await foreach (var mark in marks)
            {
                yield return mark.ToViewModel();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MarkViewModel>> Get(int id)
        {
            var lesson = await _markService.GetByIdAsync(id);

            if (lesson == null)
                return NotFound(nameof(MarkViewModel));

            return Ok(lesson.ToViewModel());
        }

        [HttpGet("[action]")]
        public async Task<double> GetAverageForStudentByLesson(int studentId, int lessonId) => await _markService.GetAverageMarkByLessonForStudent(studentId, lessonId);

        [HttpGet("[action]")]
        public async Task<double> GetAverageByLessonInGroup(int lessonId, int groupId) => await _markService.GetAverageMarkByLessonInGroup(lessonId, groupId);

        [HttpGet("[action]")]
        public async Task<double> GetAverageByLesson(int lessonId) => await _markService.GetAverageMarkForLesson(lessonId);

        [HttpGet("[action]")]
        public async Task<double> GetAverageByStudent(int studentId) => await _markService.GetAverageMarkForStudent(studentId);

        [HttpGet("[action]")]
        public async Task<double> GetAverageInGroup(int groupId) => await _markService.GetAverageMarkInGroup(groupId);

        [HttpGet("[action]")]
        public async Task<RatingByLessonViewModel> GetMostRatedLesson(int studentId) => (await _markService.GetBestLessonByMarkByStudentId(studentId)).ToViewModel();

        [HttpGet("[action]")]
        public async Task<RatingByLessonViewModel> GetLessRatedLesson(int studentId) => (await _markService.GetWorstLessonByMarkByStudentId(studentId)).ToViewModel();

        [HttpGet("[action]")]
        public async Task<double> GetProductivityForTime(int studentId, int term = 7) => await _markService.GetProductivityForTimeByStudentId(studentId, term);

        [HttpGet("[action]")]
        public async Task<bool> IsGoodStudent(int studentId) => await _markService.GetGlobalRating(studentId);

        [HttpGet("[action]")]
        public async Task<double> GetProductivityForTimeByLesson(int studentId, int lessonId, int term = 7) => await _markService.GetProductivityForTimeByLessonByStudentId(studentId, lessonId, term);

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MarkViewModel viewModel)
        {
            if (!ModelState.IsValid || viewModel.MarkDate > DateTime.Now)
                return BadRequest(ModelState);

            viewModel.Id = await _markService.CreateAsync(viewModel.ToDto());

            return CreatedAtAction(nameof(Get), viewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MarkViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            viewModel.Id = id;
            await _markService.UpdateAsync(viewModel.ToDto());

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _markService.DeleteAsync(id);

            return NoContent();
        }
    }
}
