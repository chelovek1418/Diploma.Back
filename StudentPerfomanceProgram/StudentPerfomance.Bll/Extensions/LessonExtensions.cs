using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Dal.Entities;
using System;

namespace StudentPerfomance.Bll.Extensions
{
    public static class LessonExtensions
    {
        public static LessonDto ToDto(this Lessons entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(Lessons));

            return new LessonDto
            {
                Id = entity.Id,
                Title = entity.Title
            };
        }

        public static Lessons ToEntity(this LessonDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(LessonDto));

            return new Lessons
            {
                Id = dto.Id,
                Title = dto.Title
            };
        }
    }
}
