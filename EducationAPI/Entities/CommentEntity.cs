using EducationAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Models
{
    [Table("Comment")]
    public class CommentEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public string UserId { get; set; }
        public AppUserEntity User { get; set; }
        public int CourseId { get; set; }
        public CourseEntity Course { get; set; }
    }
}
