using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.Api.Extensions;
using StudentPerfomance.Api.ViewModels;
using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Bll.Interfaces;

namespace StudentPerfomance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonsController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [HttpGet]
        public async IAsyncEnumerable<LessonViewModel> Get()
        {
            var lessons = _lessonService.GetAllAsync();

            await foreach (var lesson in lessons)
            {
                yield return lesson.ToViewModel();
            }
        }

        [HttpGet("[action]")]
        public async IAsyncEnumerable<LessonViewModel> GetByGroup(int groupId)
        {
            var lessons = _lessonService.GetLessonsByGroup(groupId);

            await foreach (var lesson in lessons)
            {
                yield return lesson.ToViewModel();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonViewModel>> Get(int id)
        {
            var lesson = await _lessonService.GetByIdAsync(id);

            if (lesson == null)
                return NotFound(nameof(LessonViewModel));

            return Ok(lesson.ToViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LessonViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            viewModel.Id = await _lessonService.CreateAsync(viewModel.ToDto());

            return CreatedAtAction(nameof(Get), viewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] LessonViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            viewModel.Id = id;
            await _lessonService.UpdateAsync(viewModel.ToDto());

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _lessonService.DeleteAsync(id);

            return NoContent();
        }
    }
}
