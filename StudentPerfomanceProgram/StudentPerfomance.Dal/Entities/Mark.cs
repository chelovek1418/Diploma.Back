using StudentPerfomance.Dal.Interfaces;
using System;

namespace StudentPerfomance.Dal.Entities
{
    public class Mark : IDbEntity
    {
        public int Id { get; set; }
        public int? Grade { get; set; }
        public DateTime Date { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }

        public Subject Subject { get; set; }
        public Student Student { get; set; }
    }
}
