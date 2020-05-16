using StudentPerfomance.Bll.Dtos;
using StudentPerfomance.Dal.Entities;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Interfaces
{
    public interface IUserService : ICrudService<User, UserDto>
    {
        Task<bool> CheckEmail(string email);
        Task<bool> CheckPhone(string phone);
    }
}
