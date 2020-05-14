using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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
                Title = entity.Title,
                Marks = entity.Marks?.Select(m => new MarkDto
                {
                    Id = m.Id,
                    LessonId = m.LessonId,
                    Mark = m.Mark,
                    MarkDate = m.MarkDate,
                    StudentId = m.StudentId
                }).ToList() ?? new List<MarkDto>()
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
