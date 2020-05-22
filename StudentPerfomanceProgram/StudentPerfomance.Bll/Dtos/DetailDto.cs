namespace StudentPerfomance.Bll.Dtos
{
    public class DetailDto
    {
        public int Id { get; set; }
        public int DayOfWeek { get; set; }
        public int Pair { get; set; }
        public bool? IsNumerator { get; set; }
        public int Semestr { get; set; }
        public GroupDto Group { get; set; }
        public LessonDto Subject { get; set; }
        public TeacherDto Teacher { get; set; }
    }
}
