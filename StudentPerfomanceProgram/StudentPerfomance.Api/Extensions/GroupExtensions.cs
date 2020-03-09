using StudentPerfomance.Api.ViewModels.GroupViewModels;
using StudentPerfomance.Bll.Dtos;
using System;

namespace StudentPerfomance.Api.Extensions
{
    public static class GroupExtensions
    {
        public static GroupDto ToDto(this GroupViewModel vm)
        {
            if (vm == null)
                throw new ArgumentNullException(nameof(GroupViewModel));

            return new GroupDto
            {
                Id = vm.Id,
                Title = vm.Title
            };
        }

        public static GroupViewModel ToViewModel(this GroupDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(GroupDto));

            return new GroupViewModel
            {
                Id = dto.Id,
                Title = dto.Title
            };
        }
    }
}
