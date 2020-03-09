using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.Api.Extensions;
using StudentPerfomance.Api.ViewModels.GroupViewModels;
using StudentPerfomance.Bll.Interfaces;

namespace StudentPerfomance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        // GET: api/Groups
        [HttpGet]
        public async IAsyncEnumerable<GroupViewModel> Get()
        {
            var groups = _groupService.GetAllAsync();

            await foreach (var group in groups)
            {
                yield return group.ToViewModel();
            }
        }

        // GET: api/Groups/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<GroupViewModel>> Get(int id)
        {
            var group = await _groupService.GetByIdAsync(id);

            if (group == null)
                return NotFound(nameof(GroupViewModel));

            return Ok(group.ToViewModel());
        }

        // POST: api/Groups
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GroupViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            viewModel.Id = await _groupService.CreateAsync(viewModel.ToDto());

            return CreatedAtAction(nameof(Get), viewModel);
        }

        // PUT: api/Groups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] GroupViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            viewModel.Id = id;
            await _groupService.UpdateAsync(viewModel.ToDto());

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _groupService.DeleteAsync(id);

            return NoContent();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddLesson([FromBody] LessonGroupViewModel viewModel)
        {
            if (viewModel == null)
                BadRequest(nameof(LessonGroupViewModel));

            await _groupService.AddLesson(viewModel.GroupId, viewModel.LessonId);

            return NoContent();
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DropLesson([FromBody] LessonGroupViewModel viewModel)
        {
            if (viewModel == null)
                BadRequest(nameof(LessonGroupViewModel));

            await _groupService.DropLesson(viewModel.GroupId, viewModel.LessonId);

            return NoContent();
        }
    }
}
