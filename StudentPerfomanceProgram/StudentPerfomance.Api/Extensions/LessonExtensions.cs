using StudentPerfomance.Api.ViewModels;
using StudentPerfomance.Api.ViewModels.MarkViewModels;
using StudentPerfomance.Bll.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

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
                Title = dto.Title,
                Marks = dto.Marks?.Select(m => new MarkViewModel
                {
                    Id = m.Id,
                    LessonId = m.LessonId,
                    Mark = m.Mark,
                    MarkDate = m.MarkDate,
                    StudentId = m.StudentId
                }).ToList() ?? new List<MarkViewModel>()
            };
        }
    }
}