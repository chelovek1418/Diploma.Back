using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.Api.Extensions;
using StudentPerfomance.Api.ViewModels.UserViewModels;
using StudentPerfomance.Bll.Interfaces;

namespace StudentPerfomance.Api.Controllers
{
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

            viewModel.Id = await _teacherService.CreateAsync(viewModel.ToDto(), Bll.Extensions.UserExtensions.ToEntity);

            return CreatedAtAction(nameof(Get), viewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TeacherViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            viewModel.Id = id;
            await _teacherService.UpdateAsync(viewModel.ToDto(), Bll.Extensions.UserExtensions.ToEntity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _teacherService.DeleteAsync(id);

            return NoContent();
        }

        #endregion
    }
}
