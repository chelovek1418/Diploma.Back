using StudentPerfomance.Api.ViewModels.MarkViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentPerfomance.Api.ViewModels.UserViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        [Required]
        public UserViewModel User { get; set; }
        public IEnumerable<MarkViewModel> Marks { get; set; }
    }
}
