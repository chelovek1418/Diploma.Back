using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Dal.DalDtos.UserDtos;
using System;

namespace StudentPerfomance.Bll.Extensions
{
    public static class UserExtensions
    {
        public static UserDto ToBllDto(this DalUserDto dalUserDto)
        {
            if (dalUserDto == null)
                throw new ArgumentNullException(nameof(DalUserDto));

            return new UserDto
            {

            };
        }

        public static DalUserDto ToDalDto(this UserDto userDto)
        {
            if (userDto == null)
                throw new ArgumentNullException(nameof(UserDto));

            return new DalUserDto
            {

            };
        }
    }
}
