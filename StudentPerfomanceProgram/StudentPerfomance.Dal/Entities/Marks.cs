using System;
using System.Collections.Generic;

namespace StudentPerfomance.Dal.Entities
{
    public partial class Marks
    {
        public int Id { get; set; }
        public byte Mark { get; set; }
        public DateTime MarkDate { get; set; }
        public int StudentId { get; set; }
        public int LessonId { get; set; }

        public virtual Lessons Lesson { get; set; }
        public virtual Students Student { get; set; }
    }
}
