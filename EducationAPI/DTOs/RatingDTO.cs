using EducationAPI.Entities;
using EducationAPI.Models;

namespace EducationAPI.DTOs
{
    public class RatingDTO
    {
        public int Id { get; set; }
        public int Start { get; set; }
        public string? Content { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
