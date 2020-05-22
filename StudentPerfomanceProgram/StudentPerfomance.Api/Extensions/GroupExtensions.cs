using StudentPerfomance.Api.ViewModels.GroupViewModels;
using StudentPerfomance.Api.ViewModels.UserViewModels;
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
                Curator = vm.Curator?.ToDto(),
                Headmen = vm.Headmen?.ToDto(),
                Faculty = vm.Faculty,
                Speciality = vm.Speciality,
                Specialization = vm.Specialization,
                Year = vm.Year,
                Students = vm.Students.Select(x => x.ToDto())
            };
        }

        public static GroupViewModel ToViewModel(this GroupDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(GroupDto));

            var group =  new GroupViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Curator = dto.Curator?.ToViewModel(),
                //Headmen = dto.Headmen?.ToViewModel(),
                Faculty = dto.Faculty,
                Speciality = dto.Speciality,
                Specialization = dto.Specialization,
                Year = dto.Year,
                //Students = dto.Students.Select(x => new StudentViewModel 
                //{
                //    User = x.User?.ToViewModel() ?? throw new NullReferenceException(nameof(StudentDto)),
                //    Id = x.Id,
                //    Marks = x.Marks?.Select(y => y.ToViewModel())
                //})
            };

            group.Students = dto.Students?.Select(x => new StudentViewModel
            {
                Id = x.Id,
                User = x.User.ToViewModel(),
                Group = group,
                Marks = x.Marks?.Select(x => x.ToViewModel())
            });

            group.Headmen = group.Students.FirstOrDefault(x => x.Id == dto.Headmen?.Id);

            return group;
        }
    }
}
