using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public static StudentDto ToDto(this Student entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(Student));

            return new StudentDto
            {
                Id = entity.Id,
                GroupId = entity.GroupId,
                User = entity.User.ToDto(),
                Marks = entity.Marks?.Select(x => x?.ToDto()) ?? new List<MarkDto>()
            };
        }

        public static Student ToEntity(this StudentDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(StudentDto));

            return new Student
            {
                Id = dto.Id,
                GroupId = dto.GroupId,
                User = dto.User.ToEntity()
            };
        }
    }
}