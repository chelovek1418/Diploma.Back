using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.Api.Constants;
using StudentPerfomance.Api.Extensions;
using StudentPerfomance.Api.Helpers;
using StudentPerfomance.Api.ViewModels.MarkViewModels;
using StudentPerfomance.Bll.Interfaces;

namespace StudentPerfomance.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MarksController : ControllerBase
    {
        private readonly IMarkService _markService;

        public MarksController(IMarkService markService)
        {
            _markService = markService;
        }

        #region CRUD

        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async IAsyncEnumerable<MarkViewModel> Get()
        {
            await foreach (var mark in _markService.GetAllAsync(Bll.Extensions.MarkExtensions.ToDto))
                yield return mark.ToViewModel();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MarkViewModel>> Get(int id)
        {
            var lesson = await _markService.GetByIdAsync(id, Bll.Extensions.MarkExtensions.ToDto);

            if (lesson == null)
                return NotFound(nameof(MarkViewModel));

            return Ok(lesson.ToViewModel());
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin + ", " + Roles.Teacher)]
        public async Task<IActionResult> Post([FromBody] MarkViewModel viewModel)
        {
            if (!ModelState.IsValid || viewModel.MarkDate > DateTime.Now)
                return BadRequest(ModelState);

            viewModel.Id = await _markService.CreateAsync(viewModel.ToDto(), Bll.Extensions.MarkExtensions.ToEntity);

            return CreatedAtAction(nameof(Get), viewModel);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin + ", " + Roles.Teacher)]
        public async Task<IActionResult> Put(int id, [FromBody] MarkViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            viewModel.Id = id;
            await _markService.UpdateAsync(viewModel.ToDto(), Bll.Extensions.MarkExtensions.ToEntity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin + ", " + Roles.Teacher)]
        public async Task<IActionResult> Delete(int id)
        {
            await _markService.DeleteAsync(id);

            return NoContent();
        }

        #endregion

        [HttpGet("[action]")]
        public async Task<IEnumerable<RatingByLessonViewModel>> GetRating(int studentId, DateTime? startDate, DateTime? endDate)
        {
            (var start, var end) = CheckDate(startDate, endDate);
            return (await _markService.GetStudentRating(studentId, start, end)).Select(x => x.ToViewModel());
        }

        [HttpGet("[action]")]
        public async IAsyncEnumerable<MarkViewModel> GetMarksForTime(int studentId, DateTime? date)
        {
            if (date == null || date.Value > DateTime.Now || date.Value < DateTime.Today.AddYears(-1))
                date = DateTimeHelper.GetTermStartDate();

            await foreach (var mark in _markService.GetMarksForTimeByStudentId(studentId, (DateTime)date, new DateTime()))
                yield return mark.ToViewModel();
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<MarkViewModel>> GetTotalMarksForGroupByLesson(int groupId, int lessonId, DateTime? startDate, DateTime? endDate)
        {
            (var start, var end) = CheckDate(startDate, endDate);
            return (await _markService.GetTotalMarksForGroupByLessonId(groupId, lessonId, start, end)).Select(x => x.ToViewModel());
        }

        [HttpGet("[action]")]
        public async IAsyncEnumerable<MarkViewModel> GetMarksForTimeByLesson(int studentId, int lessonId, DateTime? startDate, DateTime? endDate)
        {
            (var start, var end) = CheckDate(startDate, endDate);
            await foreach (var mark in _markService.GetMarksForTimeByLessonByStudentId(studentId, lessonId, start, end))
                yield return mark.ToViewModel();
        }

        [NonAction]
        private (DateTime, DateTime) CheckDate(DateTime? startDate, DateTime? endDate)
        {
            if (startDate == null || startDate.Value > DateTime.Now || startDate.Value < DateTime.Today.AddYears(-1))
                startDate = DateTimeHelper.GetTermStartDate();

            if (endDate == null || endDate.Value <= startDate.Value || endDate.Value > DateTime.Today)
                endDate = DateTime.Now;

            return (startDate.Value, endDate.Value);
        }



        [HttpGet("[action]")]
        public async Task<double> GetAverageForStudentByLesson(int studentId, int lessonId, DateTime? start, DateTime? end) => await _markService.GetAverageMarkByLessonForStudent(studentId, lessonId, start ?? DateTime.Now.AddMonths(-5), end ?? DateTime.Now);

        ////[HttpGet("[action]")]
        ////public async Task<double> GetAverageByLessonInGroup(int lessonId, int groupId) => await _markService.GetAverageMarkByLessonInGroup(lessonId, groupId);

        ////[HttpGet("[action]")]
        ////public async Task<double> GetAverageByLesson(int lessonId) => await _markService.GetAverageMarkForLesson(lessonId);

        [HttpGet("[action]")]
        public async Task<double> GetAverageByStudent(int studentId, DateTime? start, DateTime? end) => await _markService.GetAverageMarkForStudent(studentId, start ?? DateTime.Now.AddMonths(-5), end ?? DateTime.Now);

        [HttpGet("[action]")]
        public async Task<int> GetMissingsByStudent(int studentId) => await _markService.GetMissingsForStudent(studentId);

        [HttpGet("[action]")]
        public async Task<int> GetMissingsByStudentByLesson(int studentId, int lessonId) => await _markService.GetMissingsForStudentByLesson(studentId, lessonId);

        ////[HttpGet("[action]")]
        ////public async Task<double> GetAverageInGroup(int groupId) => await _markService.GetAverageMarkInGroup(groupId);

        ////[HttpGet("[action]")]
        ////public async Task<RatingByLessonViewModel> GetMostRatedLesson(int studentId) => (await _markService.GetBestLessonByMarkByStudentId(studentId)).ToViewModel();

        ////[HttpGet("[action]")]
        ////public async Task<RatingByLessonViewModel> GetLessRatedLesson(int studentId) => (await _markService.GetWorstLessonByMarkByStudentId(studentId)).ToViewModel();

        ////[HttpGet("[action]")]
        ////public async Task<double> GetProductivityForTime(int studentId, int term = 7) => await _markService.GetProductivityForTimeByStudentId(studentId, term);

        ////[HttpGet("[action]")]
        ////public async Task<bool> IsGoodStudent(int studentId) => await _markService.GetGlobalRating(studentId);

        ////[HttpGet("[action]")]
        ////public async Task<double> GetProductivityForTimeByLesson(int studentId, int lessonId, int term = 7) => await _markService.GetProductivityForTimeByLessonByStudentId(studentId, lessonId, term);
    }
}
