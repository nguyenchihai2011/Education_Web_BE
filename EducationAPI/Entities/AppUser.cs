using EducationAPI.Entities;
using EducationAPI.Enum;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Models
{
    public class AppUser : IdentityUser
    {
        public string UserType { get; set; }
        public Student? Student { get; set; }
        public Lecture? Lecture { get; set; }
        public Admin? Admin { get; set; }
        public ICollection<Comment>? Comments { get; set; }
    }
}
