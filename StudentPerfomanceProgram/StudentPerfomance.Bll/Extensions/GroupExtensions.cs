using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Dal.Entities;
using System;
using System.Linq;

namespace StudentPerfomance.Bll.Extensions
{
    public static class GroupExtensions
    {
        public static GroupDto ToDto(this Groups entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(Groups));

            return new GroupDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Students = entity.Students.Select(x => x.ToDto())
            };
        }

        public static Groups ToEntity(this GroupDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(GroupDto));

            return new Groups
            {
                Id = dto.Id,
                Title = dto.Title,
                Students = dto.Students.Select(x => x.ToEntity()).ToHashSet()
            };
        }
    }
}
