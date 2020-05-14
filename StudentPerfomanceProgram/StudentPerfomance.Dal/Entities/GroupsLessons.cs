namespace StudentPerfomance.Dal.Entities
{
    public partial class GroupsLessons
    {
        public int GroupId { get; set; }
        public int LessonId { get; set; }

        public virtual Groups Group { get; set; }
        public virtual Lessons Lesson { get; set; }
    }
}
