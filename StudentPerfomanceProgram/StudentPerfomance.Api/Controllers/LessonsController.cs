using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.Api.Constants;
using StudentPerfomance.Api.Extensions;
using StudentPerfomance.Api.Helpers;
using StudentPerfomance.Api.ViewModels;
using StudentPerfomance.Bll.Interfaces;

namespace StudentPerfomance.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonsController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        #region CRUD

        [HttpGet]
        [AllowAnonymous]
        public async IAsyncEnumerable<LessonViewModel> Get()
        {
            await foreach (var lesson in _lessonService.GetAllAsync(Bll.Extensions.LessonExtensions.ToDto))
                yield return lesson.ToViewModel();
        }        

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonViewModel>> Get(int id)
        {
            var lesson = await _lessonService.GetByIdAsync(id, Bll.Extensions.LessonExtensions.ToDto);

            if (lesson == null)
                return NotFound(nameof(LessonViewModel));

            return Ok(lesson.ToViewModel());
        }

        [HttpPost]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Post([FromBody] LessonViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            viewModel.Id = await _lessonService.CreateAsync(viewModel.ToDto(), Bll.Extensions.LessonExtensions.ToEntity);

            return CreatedAtAction(nameof(Get), viewModel);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Put(int id, [FromBody] LessonViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            viewModel.Id = id;
            await _lessonService.UpdateAsync(viewModel.ToDto(), Bll.Extensions.LessonExtensions.ToEntity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            await _lessonService.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet("[action]")]
        public async Task<int> Count() => await _lessonService.GetCount();

        #endregion

        [HttpGet("[action]")]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<bool>> CheckTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest(nameof(title));

            return Ok(await _lessonService.CheckTitleAsync(title));
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<LessonViewModel>> GetByGroup(int groupId) => (await _lessonService.GetLessonsByGroup(groupId)).Select(x => x.ToViewModel());

        [HttpGet("[action]")]
        public async Task<IEnumerable<LessonViewModel>> GetByTeacher(int teacherId) => (await _lessonService.GetByTeacher(teacherId)).Select(x => x.ToViewModel());

        [HttpGet("[action]")]
        public async Task<ActionResult<IAsyncEnumerable<LessonViewModel>>> SearchLessons(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return BadRequest();
            else
                return Ok((await _lessonService.SearchLessons(search)).Select(x => x.ToViewModel()));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<LessonViewModel>>> GetLessonsWithMarksForTimeByStudentId(int studentId, DateTime? startDate, DateTime? endDate)
        {
            if (!startDate.HasValue)
                startDate = DateTimeHelper.GetNearestEducationalMonthStartDate();

            var errorMessage = DateTimeHelper.CheckStartDate(startDate.Value);
            if (errorMessage != null)
                ModelState.AddModelError(nameof(startDate), errorMessage);

            if (!endDate.HasValue || endDate.Value < startDate.Value)
                endDate = startDate.Value.AddMonths(1);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok((await _lessonService.GetLessonsWithMarksForTimeByStudentId(studentId, startDate.Value, endDate.Value)).Select(x => x.ToViewModel()));
        }
    }
}
