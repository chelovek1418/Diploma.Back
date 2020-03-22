using StudentPerfomance.Api.ViewModels.UserViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentPerfomance.Api.ViewModels.GroupViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public IEnumerable<StudentViewModel> Students { get; set; }

        public GroupViewModel()
        {
            Students = new List<StudentViewModel>();
        }
    }
}
