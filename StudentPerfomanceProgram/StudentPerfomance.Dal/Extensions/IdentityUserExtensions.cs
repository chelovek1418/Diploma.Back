using StudentPerfomance.Dal.DalDtos.UserDtos;
using StudentPerfomance.Dal.Entities;
using System;

namespace StudentPerfomance.Dal.Extensions
{
    public static class IdentityUserExtensions
    {
        public static Users ToEntity(this DalUserDto userDto)
        {
            if (userDto == null)
                throw new ArgumentNullException(nameof(DalUserDto));

            return new Users
            {

            };
        }

        public static DalUserDto ToDalDto(this Users entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(Users));

            return new DalUserDto
            {

            };
        }
    }
}
