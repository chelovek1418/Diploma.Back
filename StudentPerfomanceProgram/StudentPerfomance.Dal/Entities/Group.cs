using StudentPerfomance.Dal.Interfaces;
using System.Collections.Generic;

namespace StudentPerfomance.Dal.Entities
{
    public class Group : IDbEntity
    {
        public Group()
        {
            GroupSubjects = new HashSet<GroupSubject>();
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Faculty { get; set; }
        public string Speciality { get; set; }
        public string Specialization { get; set; }
        public int Year { get; set; }
        public int? TeacherId { get; set; }
        public int? StudentId { get; set; }

        public Teacher Teacher { get; set; }

        public ICollection<GroupSubject> GroupSubjects { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
