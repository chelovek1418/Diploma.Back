using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Interfaces
{
    public interface ICrudService<T> where T : class
    {
        Task<int> CreateAsync(T model);

        Task<T> GetByIdAsync(int id);
    }
}
