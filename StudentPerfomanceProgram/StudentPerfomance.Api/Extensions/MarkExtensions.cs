using StudentPerfomance.Api.ViewModels.MarkViewModels;
using StudentPerfomance.Bll.Dtos;
using System;

namespace StudentPerfomance.Api.Extensions
{
    public static class MarkExtensions
    {
        public static MarkDto ToDto(this MarkViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(MarkViewModel));

            return new MarkDto
            {
                Id = viewModel.Id,
                LessonId = viewModel.LessonId,
                Mark = viewModel.Mark,
                MarkDate = viewModel.MarkDate,
                StudentId = viewModel.StudentId,
                Lesson = viewModel.Lesson?.ToDto()
            };
        }

        public static MarkViewModel ToViewModel(this MarkDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(MarkDto));

            return new MarkViewModel
            {
                Id = dto.Id,
                LessonId = dto.LessonId,
                Mark = dto.Mark,
                MarkDate = dto.MarkDate,
                StudentId = dto.StudentId,
                Lesson = dto.Lesson?.ToViewModel()
            };
        }

        public static RatingByLessonDto ToDto(this RatingByLessonViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(RatingByLessonViewModel));

            return new RatingByLessonDto
            {
                Rating = viewModel.Rating,
                LessonId = viewModel.LessonId
            };
        }

        public static RatingByLessonViewModel ToViewModel(this RatingByLessonDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(RatingByLessonDto));

            return new RatingByLessonViewModel
            {
                Rating = dto.Rating,
                LessonId = dto.LessonId
            };
        }
    }
}
