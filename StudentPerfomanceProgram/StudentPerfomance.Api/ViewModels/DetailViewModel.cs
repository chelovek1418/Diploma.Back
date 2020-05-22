using StudentPerfomance.Api.ViewModels.GroupViewModels;
using StudentPerfomance.Api.ViewModels.UserViewModels;

namespace StudentPerfomance.Api.ViewModels
{
    public class DetailViewModel
    {
        public int Id { get; set; }
        public int DayOfWeek { get; set; }
        public int Pair { get; set; }
        public bool? IsNumerator { get; set; }
        public int Semestr { get; set; }
        public GroupViewModel Group { get; set; }
        public LessonViewModel Subject { get; set; }
        public TeacherViewModel Teacher { get; set; }

    }
}
