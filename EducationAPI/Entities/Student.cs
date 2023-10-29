using EducationAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public string? AvatarUrl { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Notifycation>? Notifycations { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
        public Cart? Cart { get; set; }
        public ICollection<StudentQuiz>? StudentQuizzes { get; set; }
        public ICollection<StudentLesson>? StudentLessons { get; set; } 
    }
}
