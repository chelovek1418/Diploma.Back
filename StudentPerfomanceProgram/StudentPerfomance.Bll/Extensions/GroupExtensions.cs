using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Dal.Entities;
using System;
using System.Linq;

namespace StudentPerfomance.Bll.Extensions
{
    public static class GroupExtensions
    {
        public static GroupDto ToDto(this Group entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(Group));

            var group = new GroupDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Faculty = entity.Faculty,
                Speciality = entity.Speciality,
                Specialization = entity.Specialization,
                Year = entity.Year,
                Curator = entity.Teacher?.ToDto(),
                //Students = entity.Students?.Select(x => x?.ToDto()),
                //Headmen = entity.Students?.FirstOrDefault(x => x.Id == entity.StudentId)?.ToDto(),
            };

            group.Students = entity.Students?.Select(x => new StudentDto
            {
                User = x.User.ToDto(),
                Group = group,
                Id = x.Id,
                Marks = x.Marks?.Select(y => y.ToDto())
            });

            group.Headmen = group.Students?.FirstOrDefault(x => x.Id == entity.StudentId);

            return group;
        }

        public static Group ToEntity(this GroupDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(GroupDto));

            return new Group
            {
                Id = dto.Id,
                Title = dto.Title,
                Faculty = dto.Faculty,
                Speciality = dto.Speciality,
                Specialization = dto.Specialization,
                Year = dto.Year,
                Teacher = dto.Curator?.ToEntity(),
                StudentId = dto.Headmen?.Id,
                Students = dto.Students?.Select(x => x?.ToEntity()).ToHashSet()
            };
        }
    }
}
