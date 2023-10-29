using EducationAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Models
{
    public class Promotion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Discount { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public ICollection<Course>? Courses { get; set; }
        public int? LectureId { get; set; }
        public Lecture? Lecture { get; set; }
    }
}
