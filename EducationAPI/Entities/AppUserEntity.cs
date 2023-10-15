using EducationAPI.Entities;
using EducationAPI.Enum;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Models
{
    [Table("User")]
    public class AppUserEntity : IdentityUser
    {
        public UserType? UserType { get; set; }
        public StudentEntity? Student { get; set; }
        public LectureEntity? Lecture { get; set; }
        public AdminEntity? Admin { get; set; }
        public ICollection<CommentEntity>? Comments { get; set; }
    }
}
