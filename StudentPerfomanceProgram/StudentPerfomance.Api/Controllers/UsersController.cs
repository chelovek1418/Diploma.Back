using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentPerfomance.Api.Extensions;
using StudentPerfomance.Api.ViewModels.UserViewModels;
using StudentPerfomance.Bll.Interfaces;

namespace StudentPerfomance.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        #region CRUD

        [HttpGet]
        public async IAsyncEnumerable<UserViewModel> Get()
        {
            await foreach (var user in _userService.GetAllAsync(Bll.Extensions.UserExtensions.ToDto))
                yield return user.ToViewModel();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> Get(int id)
        {
            var user = await _userService.GetByIdAsync(id, Bll.Extensions.UserExtensions.ToDto);

            if (user == null)
                return NotFound(nameof(UserViewModel));

            return Ok(user.ToViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            viewModel.Id = await _userService.CreateAsync(viewModel.ToDto(), Bll.Extensions.UserExtensions.ToEntity);

            return CreatedAtAction(nameof(Get), viewModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            viewModel.Id = id;
            await _userService.UpdateAsync(viewModel.ToDto(), Bll.Extensions.UserExtensions.ToEntity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteAsync(id);

            return NoContent();
        }

        #endregion

        [HttpGet("[action]")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return BadRequest(nameof(email));

            return Ok(await _userService.CheckEmail(email));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<bool>> CheckPhoneNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                return BadRequest(nameof(number));

            return Ok(await _userService.CheckPhone(number));
        }

        //[HttpGet("[action]")]
        //public async IAsyncEnumerable<UserViewModel> Search(string search)
        //{
        //    IAsyncEnumerable<UserViewModel> users;

        //    if (string.IsNullOrWhiteSpace(search))
        //        users = AsyncEnumerable.Empty<UserViewModel>();
        //    else
        //        users = _userService.SearchAsync(search).Select(x=>x.ToViewModel());

        //    await foreach (var user in users)
        //        yield return user;
        //}

        //[HttpGet("[action]")]
        //public async IAsyncEnumerable<StudentViewModel> SearchStudents(string search)
        //{
        //    IAsyncEnumerable<StudentViewModel> students;

        //    if (string.IsNullOrWhiteSpace(search))
        //        students = AsyncEnumerable.Empty<StudentViewModel>();
        //    else
        //        students = _userService.SearchStudentsAsync(search).Select(x => x.ToViewModel());

        //    await foreach (var student in students)
        //        yield return student;
        //}

        //[HttpGet("[action]")]
        //public async IAsyncEnumerable<StudentViewModel> GetSudents(int take, int skip = 0)
        //{
        //    IAsyncEnumerable<StudentViewModel> students;

        //    if (take < 0 || skip < 0)
        //        students = AsyncEnumerable.Empty<StudentViewModel>();
        //    else
        //        students = _userService.GetStudents(take, skip).Select(x => x.ToViewModel());

        //    await foreach (var student in students)
        //        yield return student;
        //}

        //[HttpGet("[action]")]
        //public async IAsyncEnumerable<StudentViewModel> GetTopSudents(DateTime? date)
        //{
        //    if (!date.HasValue)
        //        date = DateTimeHelper.GetTermStartDate();

        //    await foreach (var student in _userService.GetTopStudents((DateTime)date))
        //        yield return student.ToViewModel();
        //}

        //[HttpGet("[action]")]
        //public async Task<ActionResult<StudentViewModel>> GetBestStudent()
        //{
        //    var student = await _userService.GetBestStudent();

        //    if (student == null)
        //        return NotFound(nameof(StudentViewModel));

        //    return Ok(student.ToViewModel());
        //}

        //[HttpGet("[action]")]
        //public async Task<ActionResult<StudentViewModel>> GetWorstStudent()
        //{
        //    var student = await _userService.GetWorstStudent();

        //    if (student == null)
        //        return NotFound(nameof(StudentViewModel));

        //    return Ok(student.ToViewModel());
        //}

        //[HttpGet("[action]")]
        //public async Task<ActionResult<StudentViewModel>> GetBestStudentForLessonInGroup(int lessonId, int groupId)
        //{
        //    var student = await _userService.GetBestStudentForLessonInGroup(lessonId, groupId);

        //    if (student == null)
        //        return NotFound(nameof(StudentViewModel));

        //    return Ok(student.ToViewModel());
        //}

        //[HttpGet("[action]")]
        //public async Task<ActionResult<StudentViewModel>> GetWorstStudentForLessonInGroup(int lessonId, int groupId)
        //{
        //    var student = await _userService.GetWorstStudentForLessonInGroup(lessonId, groupId);

        //    if (student == null)
        //        return NotFound(nameof(StudentViewModel));

        //    return Ok(student.ToViewModel());
        //}

        //[HttpGet("[action]")]
        //public async Task<ActionResult<StudentViewModel>> GetBestStudentForLesson(int lessonId)
        //{
        //    var student = await _userService.GetBestStudentForLesson(lessonId);

        //    if (student == null)
        //        return NotFound(nameof(StudentViewModel));

        //    return Ok(student.ToViewModel());
        //}

        //[HttpGet("[action]")]
        //public async Task<ActionResult<StudentViewModel>> GetWorstStudentForLesson(int lessonId)
        //{
        //    var student = await _userService.GetWorstStudentForLesson(lessonId);

        //    if (student == null)
        //        return NotFound(nameof(StudentViewModel));

        //    return Ok(student.ToViewModel());
        //}

        //[HttpGet("[action]")]
        //public async Task<ActionResult<StudentViewModel>> GetBestStudentInGroup(int groupId)
        //{
        //    var student = await _userService.GetBestStudentInGroup(groupId);

        //    if (student == null)
        //        return NotFound(nameof(StudentViewModel));

        //    return Ok(student.ToViewModel());
        //}

        //[HttpGet("[action]")]
        //public async Task<ActionResult<StudentViewModel>> GetWorstStudentInGroup(int groupId)
        //{
        //    var student = await _userService.GetWorstStudentInGroup(groupId);

        //    if (student == null)
        //        return NotFound(nameof(StudentViewModel));

        //    return Ok(student.ToViewModel());
        //}

        //[HttpGet("[action]")]
        //public async Task<ActionResult<StudentViewModel>> GetStudent(int id)
        //{
        //    var student = await _userService.GetStudentByIdAsync(id);

        //    if (student == null)
        //        return NotFound(nameof(StudentViewModel));

        //    return Ok(student.ToViewModel());
        //}

        //[HttpPost("[action]")]
        //public async Task<IActionResult> CreateStudent([FromBody] StudentViewModel viewModel)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    viewModel.Id = await _userService.CreateStudentAsync(viewModel.ToDto());

        //    return CreatedAtAction(nameof(Get), viewModel);
        //}

        //[HttpPut("[action]")]
        //public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentViewModel viewModel)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    viewModel.Id = id;
        //    await _userService.UpdateStudentAsync(viewModel.ToDto());

        //    return NoContent();
        //}

    }
}
