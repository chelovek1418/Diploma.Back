using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Bll.Interfaces;
using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Services
{
    public class UserService : AbstractCrudService<User, UserDto>, IUserService
    {
        new private readonly IUserRepository _repository;

        public UserService(IUserRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<bool> CheckEmail(string email) => !(await _repository.FilterAsync(x => x.Email.ToLower().Equals(email.ToLower()))).Any();

        public async Task<bool> CheckPhone(string phone) => !(await _repository.FilterAsync(x => x.PhoneNumber.ToLower().Equals(phone.ToLower()))).Any();
    }
}
