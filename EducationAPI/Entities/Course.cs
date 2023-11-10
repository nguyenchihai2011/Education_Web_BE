using EducationAPI.Entities;
using EducationAPI.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public string Level { get; set; }
        public string Language { get; set; }
        public bool IsDelete { get; set; }
        public string CreateBy { get; set; }
        public string CreateAt { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateAt { get; set; }
        public int LectureId { get; set; }
        public Lecture? Lecture { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Rating>? Ratings { get; set; }
        public ICollection<CartDetails>? CartDetails { get; set; } 
        public ICollection<OrderDetails>? OrderDetails { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Section>? Sections { get; set; }
        public int? PromotionId { get; set; }
        public Promotion? Promotion { get; set; }
    }
}
