using System.ComponentModel.DataAnnotations;

namespace StudentPerfomance.Api.ViewModels.UserViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        [Required]
        public UserViewModel User { get; set; }
    }
}
