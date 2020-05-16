using StudentPerfomance.Dal.Interfaces;
using System.Collections.Generic;

namespace StudentPerfomance.Dal.Entities
{
    public class Student : IDbEntity
    {
        public Student()
        {
            Marks = new HashSet<Mark>();
        }

        public int Id { get; set; }
        public int? GroupId { get; set; }

        public Group Group { get; set; }
        public User User { get; set; }
        public ICollection<Mark> Marks { get; set; }
    }
}
