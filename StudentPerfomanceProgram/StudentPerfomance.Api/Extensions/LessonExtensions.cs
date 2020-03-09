using StudentPerfomance.Api.ViewModels;
using StudentPerfomance.Bll.Dtos;
using System;

namespace StudentPerfomance.Api.Extensions
{
    public static class LessonExtensions
    {
        public static LessonDto ToDto(this LessonViewModel vm)
        {
            if (vm == null)
                throw new ArgumentNullException(nameof(LessonViewModel));

            return new LessonDto
            {
                Id = vm.Id,
                Title = vm.Title
            };
        }

        public static LessonViewModel ToViewModel(this LessonDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(LessonDto));

            return new LessonViewModel
            {
                Id = dto.Id,
                Title = dto.Title
            };
        }
    }
}