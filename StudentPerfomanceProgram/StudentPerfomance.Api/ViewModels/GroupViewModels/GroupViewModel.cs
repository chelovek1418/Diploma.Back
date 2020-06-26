using StudentPerfomance.Api.ViewModels.UserViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentPerfomance.Api.ViewModels.GroupViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Title length must be in the range of 2 to 20")]
        public string Title { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Faculty length must be in the range of 2 to 20")]
        public string Faculty { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Speciality length must be in the range of 2 to 30")]
        public string Speciality { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Specialization length must be in the range of 2 to 30")]
        public string Specialization { get; set; }
        [Range(1, 6, ErrorMessage = "Education year must be in the range of 1 to 6")]
        public int Year { get; set; }
        public StudentViewModel Headmen { get; set; }
        public TeacherViewModel Сurator { get; set; }

        public IEnumerable<StudentViewModel> Students { get; set; }

        public GroupViewModel()
        {
            Students = new List<StudentViewModel>();
        }
    }
}
