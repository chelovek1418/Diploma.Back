using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Interfaces;

namespace StudentPerfomance.Dal.Repository
{
    public class UserRepository : AbstractRepository<User>, IUserRepository
    {
        public UserRepository(StudentPerfomanceDbContext context) : base(context)
        {
        }
    }
}