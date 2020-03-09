using System.ComponentModel.DataAnnotations;

namespace StudentPerfomance.Api.ViewModels
{
    public class LessonViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
