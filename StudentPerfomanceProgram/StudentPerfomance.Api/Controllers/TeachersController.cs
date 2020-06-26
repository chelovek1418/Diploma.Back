using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.Api.Constants;
using StudentPerfomance.Api.Extensions;
using StudentPerfomance.Api.Helpers;
using StudentPerfomance.Api.ViewModels;
using StudentPerfomance.Api.ViewModels.UserViewModels;
using StudentPerfomance.Bll.Interfaces;

namespace StudentPerfomance.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeachersController(ITeacherService service)
        {
            _teacherService = service;
        }

        #region CRUD

        [HttpGet]
        [AllowAnonymous]
        public async IAsyncEnumerable<TeacherViewModel> Get()
        {
            await foreach (var teacher in _teacherService.GetAllAsync(Bll.Extensions.UserExtensions.ToDto))
                yield return teacher.ToViewModel();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherViewModel>> Get(int id)
        {
            var teacher = await _teacherService.GetByIdAsync(id, Bll.Extensions.UserExtensions.ToDto);

            if (teacher == null)
                return NotFound(nameof(TeacherViewModel));

            return Ok(teacher.ToViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TeacherViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = ClaimsHelper.GetIdentifier(User);
            if (id.HasValue)
            {
                viewModel.User.Id = id.Value;
                await _teacherService.CreateAsync(viewModel.ToDto(), Bll.Extensions.UserExtensions.ToEntity);
                viewModel.Id = id.Value;
                return CreatedAtAction(nameof(Get), viewModel);
            }

            return Unauthorized();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin + ", " + Roles.Teacher)]
        public async Task<IActionResult> Put(int id, [FromBody] TeacherViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            viewModel.Id = id;
            await _teacherService.UpdateAsync(viewModel.ToDto(), Bll.Extensions.UserExtensions.ToEntity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin + ", " + Roles.Teacher)]

        public async Task<IActionResult> Delete(int id)
        {
            await _teacherService.DeleteAsync(id);

            return NoContent();
        }

        [HttpGet("[action]")]
        public async Task<int> Count() => await _teacherService.GetCount();

        #endregion

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<TeacherViewModel>>> Search(string search)
        {
            search = search?.Trim();
            if (string.IsNullOrWhiteSpace(search))
                return BadRequest(nameof(search));

            return Ok((await _teacherService.SearchAsync(search.ToLower())).Select(x => x.ToViewModel()));
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<TeacherViewModel>> GetByLesson(int lessonId) => (await _teacherService.GetByLesson(lessonId)).Select(x => x.ToViewModel());

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("[action]")]
        public async Task<IEnumerable<TeacherViewModel>> GetUnconfirmedTeachers() => (await _teacherService.GetUnconfirmedTeachers()).Select(x => x.ToViewModel());

        [HttpPost("[action]")]
        [Authorize(Roles = Roles.Admin)]
        public async Task ConfirmTeacher([FromBody] int id) => await _teacherService.ConfirmTeacher(id);

        [HttpPost("[action]")]
        [Authorize(Roles = Roles.Admin + ", " + Roles.Teacher)]
        public async Task<IActionResult> AddLessonForTeacher([FromBody] TeacherLessonViewModel viewModel)
        {
            if (viewModel == null)
                BadRequest(nameof(TeacherLessonViewModel));

            await _teacherService.AddLesson(viewModel.TeacherId, viewModel.LessonId);

            return NoContent();
        }

        [HttpPost("[action]")]
        [Authorize(Roles = Roles.Admin + ", " + Roles.Teacher)]
        public async Task<IActionResult> DropLessonForTeacher([FromBody] TeacherLessonViewModel viewModel)
        {
            if (viewModel == null)
                BadRequest(nameof(TeacherLessonViewModel));

            await _teacherService.DropLesson(viewModel.TeacherId, viewModel.LessonId);

            return NoContent();
        }
    }
}
