using StudentPerfomance.Dal.Data;

namespace StudentPerfomance.Dal.Repository
{
    public abstract class ContextProvider
    {
        protected StudentPerfomanceDbContext dbContext;

        protected ContextProvider(StudentPerfomanceDbContext context)
        {
            dbContext = context;
        }
    }
}
