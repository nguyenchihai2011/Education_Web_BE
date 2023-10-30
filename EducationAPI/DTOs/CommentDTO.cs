using EducationAPI.Models;

namespace EducationAPI.DTOs
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string UserId { get; set; }
        public int CourseId { get; set; }
    }
}
