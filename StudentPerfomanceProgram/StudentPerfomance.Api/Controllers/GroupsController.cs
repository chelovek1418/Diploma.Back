using System;
using System.Collections.Generic;
using System.Linq;
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

        #region CRUD

        // GET: api/Groups
        [HttpGet]
        public async IAsyncEnumerable<GroupViewModel> Get()
        {
            await foreach (var group in _groupService.GetAllAsync(Bll.Extensions.GroupExtensions.ToDto))
                yield return group.ToViewModel();
        }

        // GET: api/Groups/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<GroupViewModel>> Get(int id)
        {
            var group = await _groupService.GetByIdAsync(id, Bll.Extensions.GroupExtensions.ToDto);

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

            viewModel.Id = await _groupService.CreateAsync(viewModel.ToDto(), Bll.Extensions.GroupExtensions.ToEntity);

            return CreatedAtAction(nameof(Get), viewModel);
        }

        // PUT: api/Groups/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] GroupViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            viewModel.Id = id;
            await _groupService.UpdateAsync(viewModel.ToDto(), Bll.Extensions.GroupExtensions.ToEntity);

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _groupService.DeleteAsync(id);

            return NoContent();
        }

        #endregion

        [HttpGet("[action]")]
        public async Task<ActionResult<GroupViewModel>> GetWithMarksByLesson(int groupId, int lessonId, DateTime? date)
        {
            if (!date.HasValue || date.Value.Month > DateTime.Today.Month)
                date = DateTime.Today;

            if (groupId < 0 || lessonId < 0)
                return BadRequest($"{nameof(groupId)} or/and {nameof(lessonId)}");

            return Ok((await _groupService.GetWithMarksByLesson(groupId, lessonId, date.Value)).ToViewModel());
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<GroupViewModel>>> SearchGroups(string search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return BadRequest();
            else
                return Ok((await _groupService.SearchGroupsAsync(search)).Select(x => x.ToViewModel()));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<bool>> CheckTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest(nameof(title));

            return Ok(await _groupService.CheckTitleAsync(title));
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<GroupViewModel>> GetByLesson(int lessonId) => (await _groupService.GetByLessonAsync(lessonId)).Select(x => x.ToViewModel());

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
