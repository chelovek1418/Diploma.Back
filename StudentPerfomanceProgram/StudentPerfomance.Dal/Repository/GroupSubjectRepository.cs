using StudentPerfomance.Dal.Entities;
using StudentPerfomance.Dal.Interfaces;

namespace StudentPerfomance.Dal.Repository
{
    public class GroupSubjectRepository : AbstractRepository<GroupSubject>, IGroupSubjectRepository
    {
        public GroupSubjectRepository(StudentPerfomanceDbContext context) : base(context)
        {
        }
    }
}
