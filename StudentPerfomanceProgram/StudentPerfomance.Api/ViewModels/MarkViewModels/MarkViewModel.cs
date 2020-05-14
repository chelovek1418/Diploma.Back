using System;

namespace StudentPerfomance.Api.ViewModels.MarkViewModels
{
    public class MarkViewModel
    {
        public int Id { get; set; }
        public int Mark { get; set; }
        public DateTime MarkDate { get; set; }
        public int StudentId { get; set; }
        public int LessonId { get; set; }
        public LessonViewModel Lesson { get; set; }
    }
}
