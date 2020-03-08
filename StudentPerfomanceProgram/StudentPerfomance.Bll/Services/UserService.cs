using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Bll.Extensions;
using StudentPerfomance.Bll.Interfaces;
using StudentPerfomance.Dal.DalDtos.UserDtos;
using StudentPerfomance.Dal.Repository.Interfaces;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Services
{
    public class UserService : CommonService<UserDto, DalUserDto>
    {
        public UserService(IRepository<DalUserDto> repo) : base(repo)
        {
        }

        public override async Task<int> CreateAsync(UserDto newUser) => await repository.CreateAsync(newUser.ToDalDto());

        public override async Task<UserDto> GetByIdAsync(int id) => (await repository.GetByIdAsync(id)).ToBllDto();
    }
}
