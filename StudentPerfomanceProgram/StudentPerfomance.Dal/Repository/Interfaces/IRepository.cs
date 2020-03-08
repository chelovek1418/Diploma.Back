using System.Threading.Tasks;

namespace StudentPerfomance.Dal.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<int> CreateAsync(T model);

        Task<T> GetByIdAsync(int id);
    }
}
