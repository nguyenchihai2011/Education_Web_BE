using EducationAPI.Entities;
using EducationAPI.Models;

namespace EducationAPI.DTOs
{
    public class LectureDTO
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public string? AvatarUrl { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string UserId { get; set; }
        public AppUserEntity? User { get; set; }
        public ICollection<NotifycationEntity>? Notifycations { get; set; }
        public ICollection<CourseEntity>? Courses { get; set; }
    }
}
