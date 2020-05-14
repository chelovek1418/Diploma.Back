using System;

namespace StudentPerfomance.Bll.Dtos
{
    public class MarkDto
    {
        public int Id { get; set; }
        public int Mark { get; set; }
        public DateTime MarkDate { get; set; }
        public int StudentId { get; set; }
        public int LessonId { get; set; }
        public LessonDto Lesson { get; set; }
    }
}
