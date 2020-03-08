using StudentPerfomance.Bll.Interfaces;
using StudentPerfomance.Dal.Repository.Interfaces;
using System.Threading.Tasks;

namespace StudentPerfomance.Bll.Services
{
    public abstract class CommonService<TBll, TDal> : ICrudService<TBll> where TBll : class where TDal : class
    {
        protected IRepository<TDal> repository;

        public CommonService(IRepository<TDal> repo)
        {
            repository = repo;
        }

        public abstract Task<int> CreateAsync(TBll model);

        public abstract Task<TBll> GetByIdAsync(int id);
    }
}
