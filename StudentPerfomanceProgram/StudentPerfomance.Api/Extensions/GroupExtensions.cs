﻿using StudentPerfomance.Api.ViewModels.GroupViewModels;
using StudentPerfomance.Bll.Dtos;
using System;
using System.Linq;

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
                Title = vm.Title,
                Students = vm.Students.Select(x => x.ToDto())
            };
        }

        public static GroupViewModel ToViewModel(this GroupDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(GroupDto));

            return new GroupViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Students = dto.Students.Select(x=>x.ToViewModel())
            };
        }
    }
}
