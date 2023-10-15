using EducationAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Models
{
    [Table("Promoiton")]
    public class PromotionEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Discount { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public ICollection<CourseEntity>? Courses { get; set; }
        public int? LectureId { get; set; }
        public LectureEntity? Lecture { get; set; }
    }
}
