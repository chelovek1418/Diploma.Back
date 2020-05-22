using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Dal.Entities;
using System;

namespace StudentPerfomance.Bll.Extensions
{
    public static class DetailExtensions
    {
        public static DetailDto ToDto(this Detail entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(Detail));

            return new DetailDto
            {
                Id = entity.Id,
                DayOfWeek = entity.DayOfWeek,
                IsNumerator = entity.IsNumerator,
                Pair = entity.Pair,
                Semestr = entity.Semestr,
                Group = entity.GroupSubject?.Group?.ToDto(),
                Subject = entity.GroupSubject?.Subject?.ToDto(),
                Teacher = entity.Teacher?.ToDto()
            };
        }

        public static Detail ToEntity(this DetailDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(DetailDto));

            return new Detail
            {
                Id = dto.Id,
                DayOfWeek = dto.DayOfWeek,
                IsNumerator = dto.IsNumerator,
                Pair = dto.Pair,
                Semestr = dto.Semestr
            };
        }
    }
}
