using System.Collections.Generic;

namespace StudentPerfomance.Dal.Entities
{
    public partial class Lessons
    {
        public Lessons()
        {
            GroupsLessons = new HashSet<GroupsLessons>();
            Marks = new HashSet<Marks>();
            UsersLessons = new HashSet<UsersLessons>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public virtual ICollection<GroupsLessons> GroupsLessons { get; set; }
        public virtual ICollection<Marks> Marks { get; set; }
        public virtual ICollection<UsersLessons> UsersLessons { get; set; }
    }
}
