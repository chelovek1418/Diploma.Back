namespace StudentPerfomance.Dal.Entities
{
    public partial class UsersLessons
    {
        public int UserId { get; set; }
        public int LessonId { get; set; }

        public virtual Lessons Lesson { get; set; }
        public virtual User User { get; set; }
    }
}
