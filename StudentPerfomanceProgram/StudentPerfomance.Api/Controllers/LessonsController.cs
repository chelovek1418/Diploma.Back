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
        private readonly ICrudService<LessonDto> _crudService;

        public LessonsController(ICrudService<LessonDto> crudService)
        {
            _crudService = crudService;
        }

        [HttpGet]
        public async IAsyncEnumerable<LessonViewModel> Get()
        {
            var lessons = _crudService.GetAllAsync();

            await foreach (var lesson in lessons)
            {
                yield return lesson.ToViewModel();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonViewModel>> Get(int id)
        {
            var lesson = await _crudService.GetByIdAsync(id);

            if (lesson == null)
                return NotFound(nameof(LessonViewModel));

            return Ok(lesson.ToViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LessonViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            viewModel.Id = await _crudService.CreateAsync(viewModel.ToDto());

            return CreatedAtAction(nameof(Get), viewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] LessonViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            viewModel.Id = id;
            await _crudService.UpdateAsync(viewModel.ToDto());

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _crudService.DeleteAsync(id);

            return NoContent();
        }
    }
}
