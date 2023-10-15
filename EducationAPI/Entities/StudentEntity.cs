using EducationAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Entities
{
    [Table("Student")]
    public class StudentEntity
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public string? AvatarUrl { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserId { get; set; }
        public AppUserEntity User { get; set; }
        public ICollection<OrderEntity>? Orders { get; set; }
        public ICollection<NotifycationEntity>? Notifycations { get; set; }
        public ICollection<RatingEntity>? Ratings { get; set; }
        public CartEntity? Cart { get; set; }
        public ICollection<StudentQuizEntity>? StudentQuizzes { get; set; }
        public ICollection<StudentLessonEntity>? StudentLessons { get; set; } 
    }
}
