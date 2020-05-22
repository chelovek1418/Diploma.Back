using StudentPerfomance.Dal.Interfaces;
using System.Collections.Generic;

namespace StudentPerfomance.Dal.Entities
{
    public class Teacher : IDbEntity
    {
        public Teacher()
        {
            TeacherSubjects = new HashSet<TeacherSubject>();
            Details = new HashSet<Detail>();
        }

        public int Id { get; set; }
        public string Position { get; set; }
        public bool IsConfirmed { get; set; }
        public User User { get; set; }
        public ICollection<TeacherSubject> TeacherSubjects { get; set; }
        public ICollection<Detail> Details { get; set; }
    }
}
