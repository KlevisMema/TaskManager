using System.ComponentModel.DataAnnotations;

namespace TaskManager.DAL.DTO_s.User
{
    public class UserCreateDto
    {
        [StringLength(50)]
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(50)]
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}