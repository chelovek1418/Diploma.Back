using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Dal.Entities;
using System;

namespace StudentPerfomance.Bll.Extensions
{
    public static class MarkExtensions
    {
        public static MarkDto ToDto(this Mark entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(Mark));

            return new MarkDto
            {
                Id = entity.Id,
                LessonId = entity.SubjectId,
                Mark = entity.Grade,
                MarkDate = entity.Date,
                StudentId = entity.StudentId,
                Lesson = entity.Subject?.ToDto()
            };
        }

        public static Mark ToEntity(this MarkDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(MarkDto));

            return new Mark
            {
                Id = dto.Id,
                SubjectId = dto.LessonId,
                Grade = (byte)dto.Mark,
                Date = dto.MarkDate,
                StudentId = dto.StudentId,
                Subject = dto.Lesson?.ToEntity()
            };
        }
    }
}
