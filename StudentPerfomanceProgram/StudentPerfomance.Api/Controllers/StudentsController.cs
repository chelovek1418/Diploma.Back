using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.Api.Constants;
using StudentPerfomance.Api.Extensions;
using StudentPerfomance.Api.Helpers;
using StudentPerfomance.Api.ViewModels.UserViewModels;
using StudentPerfomance.Bll.Interfaces;

namespace StudentPerfomance.Api.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        public async IAsyncEnumerable<StudentViewModel> Get()
        {
            await foreach (var user in _studentService.GetAllAsync(Bll.Extensions.UserExtensions.ToDto))
                yield return user.ToViewModel();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<StudentViewModel>> Get(int id)
        {
            var user = await _studentService.GetByIdAsync(id, Bll.Extensions.UserExtensions.ToDto);

            if (user == null)
                return NotFound(nameof(StudentViewModel));

            return Ok(user.ToViewModel());
        }

        [HttpPost]
        [Authorize(Roles = Roles.Student)]
        public async Task<IActionResult> Post([FromBody] StudentViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = ClaimsHelper.GetIdentifier(User);
            if (id.HasValue)
            {
                viewModel.User.Id = id.Value;
                await _studentService.CreateAsync(viewModel.ToDto(), Bll.Extensions.UserExtensions.ToEntity);
                viewModel.Id = id.Value;
                return CreatedAtAction(nameof(Get), viewModel);
            }

            return Unauthorized();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Admin + ", " + Roles.Student)]
        public async Task<IActionResult> Put(int id, [FromBody] StudentViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            viewModel.Id = id;
            await _studentService.UpdateAsync(viewModel.ToDto(), Bll.Extensions.UserExtensions.ToEntity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Admin + ", " + Roles.Student)]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("[action]")]
        public async Task<int> Count() => await _studentService.GetCount();

        #endregion

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<StudentViewModel>>> Search(string search)
        {
            search = search?.Trim();

            if (string.IsNullOrWhiteSpace(search))
                return BadRequest("Empty search word");

            return Ok((await _studentService.FilterAsync(search.ToLower())).Select(x => x.ToViewModel()));
        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<StudentViewModel>>> GetBestStudents(DateTime? startDate, DateTime? endDate)
        {
            if (!startDate.HasValue)
                startDate = DateTimeHelper.GetTermStartDate();
            else
            {
                var errorMessage = DateTimeHelper.CheckStartDate(startDate.Value);
                if (errorMessage != null)
                    return BadRequest(errorMessage);
            }

            if (!endDate.HasValue || endDate.Value < startDate.Value)
                endDate = DateTime.Now;

            return Ok((await _studentService.GetTopStudents(startDate.Value, endDate.Value)).Select(x => x.ToViewModel()));
        }
    }
}
