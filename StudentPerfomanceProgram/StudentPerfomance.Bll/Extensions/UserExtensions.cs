using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Dal.Entities;
using System;

namespace StudentPerfomance.Bll.Extensions
{
    public static class UserExtensions
    {
        public static UserDto ToDto(this User entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(User));

            return new UserDto
            {
                Id = entity.Id,
                Email = entity.Email,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
            };
        }

        public static User ToEntity(this UserDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(UserDto));

            return new User
            {
                Id = dto.Id,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };
        }

        public static StudentDto ToDto(this Students entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(Students));

            return new StudentDto
            {
                Id = entity.Id,
                GroupId = entity.GroupId,
                User = entity.IdNavigation.ToDto()
            };
        }

        public static Students ToEntity(this StudentDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(StudentDto));

            return new Students
            {
                Id = dto.Id,
                GroupId = dto.GroupId,
                IdNavigation = dto.User.ToEntity()
            };
        }
    }
}