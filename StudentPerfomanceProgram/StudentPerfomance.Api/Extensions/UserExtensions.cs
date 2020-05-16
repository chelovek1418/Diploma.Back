using StudentPerfomance.Api.ViewModels.MarkViewModels;
using StudentPerfomance.Api.ViewModels.UserViewModels;
using StudentPerfomance.Bll.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

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
                Patronymic = viewModel.Patronymic,
                Department = viewModel.Department,
                PhoneNumber = viewModel.PhoneNumber,
                Photo = viewModel.Photo
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
                Patronymic = dto.Patronymic,
                Department = dto.Department,
                PhoneNumber = dto.PhoneNumber,
                Photo = dto.Photo
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
                User = dto.User.ToViewModel(),
                Marks = dto.Marks?.Select(x => x?.ToViewModel()) ?? new List<MarkViewModel>()
            };
        }

        public static TeacherDto ToDto(this TeacherViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(TeacherViewModel));

            return new TeacherDto
            {
                Id = viewModel.Id,
                Position = viewModel.Position,
                User = viewModel.User.ToDto()
            };
        }

        public static TeacherViewModel ToViewModel(this TeacherDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(TeacherDto));

            return new TeacherViewModel
            {
                Id = dto.Id,
                Position = dto.Position,
                User = dto.User.ToViewModel()
            };
        }
    }
}
