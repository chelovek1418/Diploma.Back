using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentPerfomance.Bll.Extensions
{
    public static class LessonExtensions
    {
        public static LessonDto ToDto(this Subject entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(Subject));

            return new LessonDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Marks = entity.Marks?.Select(m => new MarkDto
                {
                    Id = m.Id,
                    LessonId = m.SubjectId,
                    Mark = m.Grade,
                    MarkDate = m.Date,
                    StudentId = m.StudentId
                }).ToList() ?? new List<MarkDto>()
            };
        }

        public static Subject ToEntity(this LessonDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(LessonDto));

            return new Subject
            {
                Id = dto.Id,
                Title = dto.Title
            };
        }
    }
}
