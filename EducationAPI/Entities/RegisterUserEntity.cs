using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Models
{
    [Table("Register")]
    public class RegisterUserEntity
    {
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
