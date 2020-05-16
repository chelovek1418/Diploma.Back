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
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService service)
        {
            _studentService = service;
        }

        #region CRUD

        [HttpGet]
        public async IAsyncEnumerable<StudentViewModel> Get()
        {
            await foreach (var user in _studentService.GetAllAsync(Bll.Extensions.UserExtensions.ToDto))
                yield return user.ToViewModel();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentViewModel>> Get(int id)
        {
            var user = await _studentService.GetByIdAsync(id, Bll.Extensions.UserExtensions.ToDto);

            if (user == null)
                return NotFound(nameof(StudentViewModel));

            return Ok(user.ToViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StudentViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            viewModel.Id = await _studentService.CreateAsync(viewModel.ToDto(), Bll.Extensions.UserExtensions.ToEntity);

            return CreatedAtAction(nameof(Get), viewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] StudentViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            viewModel.Id = id;
            await _studentService.UpdateAsync(viewModel.ToDto(), Bll.Extensions.UserExtensions.ToEntity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.DeleteAsync(id);

            return NoContent();
        }

        #endregion
    }
}
