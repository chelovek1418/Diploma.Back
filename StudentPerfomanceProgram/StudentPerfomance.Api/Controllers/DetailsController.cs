using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.Api.Extensions;
using StudentPerfomance.Api.Helpers;
using StudentPerfomance.Api.ViewModels;
using StudentPerfomance.Bll.Interfaces;

namespace StudentPerfomance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailsController : ControllerBase
    {
        private readonly IDetailService _detailService;

        public DetailsController(IDetailService service)
        {
            _detailService = service;
        }

        #region CRUD

        [HttpGet]
        public async IAsyncEnumerable<DetailViewModel> Get()
        {
            await foreach (var detail in _detailService.GetAllAsync(Bll.Extensions.DetailExtensions.ToDto))
                yield return detail.ToViewModel();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetailViewModel>> Get(int id)
        {
            var user = await _detailService.GetByIdAsync(id, Bll.Extensions.DetailExtensions.ToDto);

            if (user == null)
                return NotFound(nameof(DetailViewModel));

            return Ok(user.ToViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DetailViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            viewModel.Id = await _detailService.CreateAsync(viewModel.ToDto(), Bll.Extensions.DetailExtensions.ToEntity);

            return CreatedAtAction(nameof(Get), viewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] DetailViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            viewModel.Id = id;
            await _detailService.UpdateAsync(viewModel.ToDto(), Bll.Extensions.DetailExtensions.ToEntity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _detailService.DeleteAsync(id);

            return NoContent();
        }

        #endregion

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<DetailViewModel>>> GetScheduleForGroup(int groupId, int? semestr)
        {
            if (groupId < 0 || semestr < 0 || semestr > 3)
                return BadRequest();

            if (!semestr.HasValue)
                semestr = DateTimeHelper.GetCurrentSemestr();

            return Ok((await _detailService.GetScheduleForGroup(groupId, semestr.Value)).Select(x => x.ToViewModel()));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<DetailViewModel>>> GetScheduleForTeacher(int teacherId, int? semestr)
        {
            if (teacherId < 0 || semestr < 0 || semestr > 3)
                return BadRequest();

            if (!semestr.HasValue)
                semestr = DateTimeHelper.GetCurrentSemestr();

            return Ok((await _detailService.GetScheduleForTeacher(teacherId, semestr.Value)).Select(x => x.ToViewModel()));
        }
    }
}
