using StudentPerfomance.Api.ViewModels;
using StudentPerfomance.Bll.Dtos;
using System;

namespace StudentPerfomance.Api.Extensions
{
    public static class DetailExtensions
    {
        public static DetailDto ToDto(this DetailViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(DetailViewModel));

            return new DetailDto
            {
                Id = viewModel.Id,
                DayOfWeek = viewModel.DayOfWeek,
                IsNumerator = viewModel.IsNumerator,
                Pair = viewModel.Pair,
                Semestr = viewModel.Semestr,
                Group = viewModel.Group?.ToDto(),
                Subject = viewModel.Subject?.ToDto(),
                Teacher = viewModel.Teacher?.ToDto()
            };
        }

        public static DetailViewModel ToViewModel (this DetailDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(DetailDto));

            return new DetailViewModel
            {
                Id = dto.Id,
                DayOfWeek = dto.DayOfWeek,
                IsNumerator = dto.IsNumerator,
                Pair = dto.Pair,
                Semestr = dto.Semestr,
                Group = dto.Group?.ToViewModel(),
                Subject = dto.Subject?.ToViewModel(),
                Teacher = dto.Teacher?.ToViewModel()
            };
        }
    }
}
