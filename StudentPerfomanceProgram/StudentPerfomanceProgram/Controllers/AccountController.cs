//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using StudentPerfomance.Api.Extensions;
//using StudentPerfomance.Api.ViewModels.UserViewModels;
//using StudentPerfomance.Bll.Dtos;
//using StudentPerfomance.Bll.Interfaces;
//using StudentPerfomance.Dal.Repository;

//namespace StudentPerfomanceProgram.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class AccountController : ControllerBase
//    {
//        private readonly StudentManagementRepository _repository;
//        private readonly ICrudService<UserDto> _crudService;

//        public AccountController(StudentManagementRepository repository, ICrudService<UserDto> crudService)
//        {
//            _repository = repository;
//            _crudService = crudService;
//        }

//        public async Task<IActionResult> Register(RegisterUserViewModel registerViewModel)
//        {
//            var userDto = new UserDto
//            {
//                LastName = registerViewModel.LastName,
//                FirstName = registerViewModel.LastName,
//                Email = registerViewModel.LastName,
//                Password = registerViewModel.Password
//            };

//            var userViewModel = userDto.ToBaseViewModel();
//            userViewModel.Id = await _crudService.CreateAsync(userDto);

//            return CreatedAtAction(nameof(GetById), new { id = userViewModel.Id }, userViewModel);
//        }

//        [HttpGet]
//        public async Task<IActionResult> GetById(int? id)
//        {
//            if (!id.HasValue)
//                throw new ArgumentNullException(nameof(id));

//            return Ok((await _crudService.GetByIdAsync(id.Value)).ToBaseViewModel());
//        }

//        [HttpGet]
//        public async Task<int?> GetBestStudentAsync() => await Task.Run(() => _repository.GetBestStudentId());
//    }
//}
