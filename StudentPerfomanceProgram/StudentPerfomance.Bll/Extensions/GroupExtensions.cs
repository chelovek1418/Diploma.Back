using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Dal.Entities;
using System;

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
                Title = entity.Title
            };
        }

        public static Groups ToEntity(this GroupDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(GroupDto));

            return new Groups
            {
                Id = dto.Id,
                Title = dto.Title
            };
        }
    }
}
