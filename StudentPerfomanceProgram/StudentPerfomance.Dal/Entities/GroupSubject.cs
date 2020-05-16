using StudentPerfomance.Dal.Interfaces;
using System.Collections.Generic;

namespace StudentPerfomance.Dal.Entities
{
    public class GroupSubject : IDbEntity
    {
        public GroupSubject()
        {
            Details = new HashSet<Detail>();
        }

        public int Id { get; set; }
        public int GroupId { get; set; }
        public int SubjectId { get; set; }

        public Group Group { get; set; }
        public Subject Subject { get; set; }
        public ICollection<Detail> Details { get; set; }
    }
}
