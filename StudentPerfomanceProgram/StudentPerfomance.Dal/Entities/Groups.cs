using System;
using System.Collections.Generic;

namespace StudentPerfomance.Dal.Entities
{
    public partial class Groups
    {
        public Groups()
        {
            GroupsLessons = new HashSet<GroupsLessons>();
            Students = new HashSet<Students>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<GroupsLessons> GroupsLessons { get; set; }
        public virtual ICollection<Students> Students { get; set; }
    }
}
