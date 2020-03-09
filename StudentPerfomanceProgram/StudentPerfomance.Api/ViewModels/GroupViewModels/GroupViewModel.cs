using System.ComponentModel.DataAnnotations;

namespace StudentPerfomance.Api.ViewModels.GroupViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
