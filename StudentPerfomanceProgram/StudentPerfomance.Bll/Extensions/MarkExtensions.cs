using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Dal.Entities;
using System;

namespace StudentPerfomance.Bll.Extensions
{
    public static class MarkExtensions
    {
        public static MarkDto ToDto(this Marks entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(Marks));

            return new MarkDto
            {
                Id = entity.Id,
                LessonId = entity.LessonId,
                Mark = entity.Mark,
                MarkDate = entity.MarkDate,
                StudentId = entity.StudentId,
                Lesson = entity.Lesson?.ToDto()
            };
        }

        public static Marks ToEntity(this MarkDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(MarkDto));

            return new Marks
            {
                Id = dto.Id,
                LessonId = dto.LessonId,
                Mark = dto.Mark,
                MarkDate = dto.MarkDate,
                StudentId = dto.StudentId,
                Lesson = dto.Lesson?.ToEntity()
            };
        }

        public static RatingByLessonDto ToDto(this RatingByLesson entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(RatingByLesson));

            return new RatingByLessonDto
            {
                Id = entity.Id,
                Rating = entity.Rating,
                Title = entity.Title
            };
        }

        public static RatingByLesson ToEntity(this RatingByLessonDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(RatingByLessonDto));

            return new RatingByLesson
            {
                Id = dto.Id,
                Rating = (int)dto.Rating,
                Title = dto.Title
            };
        }
    }
}
