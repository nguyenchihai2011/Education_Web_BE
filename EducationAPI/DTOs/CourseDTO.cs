using EducationAPI.Entities;
using EducationAPI.Enum;
using EducationAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Data
{
    [Table("Course")]
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public Level Level { get; set; }
        public string Language { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateAt { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateAt { get; set; }
        public int LectureId { get; set; }
        public LectureEntity? Lecture { get; set; }
        public ICollection<CommentEntity>? Comments { get; set; }
        public ICollection<RatingEntity>? Ratings { get; set; }
        public ICollection<CartDetailsEntity>? CartDetails { get; set; }
        public ICollection<OrderDetailsEntity>? OrderDetails { get; set; }
        public int CategoryId { get; set; }
        public CategoryEntity? Category { get; set; }
        public ICollection<SectionEntity>? Sections { get; set; }
        public int? PromotionId { get; set; }
        public PromotionEntity? Promotion { get; set; }

    }
}
