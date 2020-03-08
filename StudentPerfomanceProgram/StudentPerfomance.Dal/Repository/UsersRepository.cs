using Microsoft.AspNetCore.Identity;
using StudentPerfomance.Dal.DalDtos.UserDtos;
using StudentPerfomance.Dal.Data;
using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Extensions;
using StudentPerfomance.Dal.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Repository
{
    public class UsersRepository : ContextProvider, IRepository<DalUserDto>
    {
        private readonly UserManager<Users> userManager;

        public UsersRepository(UserManager<Users> manager, StudentPerfomanceDbContext context) : base(context)
        {
            userManager = manager;
        }

        public async Task<int> CreateAsync(DalUserDto dalUser)
        {
            if (dalUser == null)
                throw new ArgumentNullException(nameof(DalUserDto));

            var identityUser = dalUser.ToEntity();

            if ((await userManager.CreateAsync(identityUser, dalUser.Password)).Succeeded)
                return identityUser.Id;

            throw new Exception(nameof(DalUserDto));
        }

        public async Task<DalUserDto> GetByIdAsync(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            if (user == null)
                throw new NullReferenceException(nameof(DalUserDto));

            return user.ToDalDto();
        }
    }
}
