using System.ComponentModel.DataAnnotations;

namespace StudentPerfomance.Api.ViewModels.UserViewModels
{
    public class TeacherViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Position length must be in the range of 2 to 30")]
        public string Position { get; set; }
        [Required]
        public UserViewModel User { get; set; }
    }
}
