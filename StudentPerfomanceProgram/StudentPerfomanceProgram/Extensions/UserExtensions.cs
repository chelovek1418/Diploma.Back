using StudentPerfomance.Api.ViewModels.UserViewModels;
using StudentPerfomance.Bll.Dtos;
using System;

namespace StudentPerfomance.Api.Extensions
{
    public static class UserExtensions
    {
        public static UserViewModel ToBaseViewModel(this UserDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(UserDto));

            return new UserViewModel
            {
                Id = dto.Id,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };
        }

        public static UserDto ToDto(this UserViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(UserDto));

            return new UserDto
            {
                //Id = dto.Id
            };
        }
    }
}
