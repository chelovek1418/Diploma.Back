using StudentPerfomance.Dal.Interfaces;
using System.Collections.Generic;

namespace StudentPerfomance.Dal.Entities
{
    public class Subject : IDbEntity
    {
        public Subject()
        {
            GroupSubjects = new HashSet<GroupSubject>();
            Marks = new HashSet<Mark>();
            TeacherSubjects = new HashSet<TeacherSubject>();
        }

        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<GroupSubject> GroupSubjects { get; set; }
        public ICollection<Mark> Marks { get; set; }
        public ICollection<TeacherSubject> TeacherSubjects { get; set; }
    }
}
