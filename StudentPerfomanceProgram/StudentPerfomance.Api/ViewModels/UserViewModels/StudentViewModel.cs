using StudentPerfomance.Api.ViewModels.GroupViewModels;
using StudentPerfomance.Api.ViewModels.MarkViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentPerfomance.Api.ViewModels.UserViewModels
{
    public class StudentViewModel
    {
        public int Id { get; set; }
        public GroupViewModel Group { get; set; }
        [Required]
        public UserViewModel User { get; set; }
        public IEnumerable<MarkViewModel> Marks { get; set; }
    }
}
