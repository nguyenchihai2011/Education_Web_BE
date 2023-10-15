using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Models
{
    [Table("Login")]
    public class LoginEntity
    {
        [Required(ErrorMessage = "Username is required!")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        public string? Password { get; set; }
    }
}
