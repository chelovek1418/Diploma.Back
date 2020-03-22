using StudentPerfomance.Api.ViewModels.UserViewModels;
using StudentPerfomance.Bll.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPerfomance.Api.Extensions
{
    public static class UserExtensions
    {
        public static UserDto ToDto(this UserViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(UserViewModel));

            return new UserDto
            {
                Id = viewModel.Id,
                Email = viewModel.Email,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
            };
        }

        public static UserViewModel ToViewModel(this UserDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(UserDto));

            return new UserViewModel
            {
                Id = dto.Id,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };
        }

        public static StudentDto ToDto(this StudentViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(StudentViewModel));

            return new StudentDto
            {
                Id = viewModel.Id,
                GroupId = viewModel.GroupId,
                User = viewModel.User.ToDto()
            };
        }

        public static StudentViewModel ToViewModel(this StudentDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(StudentDto));

            return new StudentViewModel
            {
                Id = dto.Id,
                GroupId = dto.GroupId,
                User = dto.User.ToViewModel()
            };
        }
    }
}
