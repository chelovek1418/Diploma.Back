using System.ComponentModel.DataAnnotations;

namespace StudentPerfomance.Api.ViewModels.UserViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "First name length must be in the range of 2 to 20")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Last name length must be in the range of 2 to 20")]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(30, ErrorMessage = "Email length must not exceed 30 characters")]
        public string Email { get; set; }
        [StringLength(30, ErrorMessage = "Patronymic length must be less then 30 characters")]
        public string Patronymic { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Department length must be in the range of 2 to 20")]
        public string Department { get; set; }
        public byte[] Photo { get; set; }
    }
}
