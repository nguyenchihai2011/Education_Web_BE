using EducationAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Entities
{
    [Table("Section")]
    public class SectionEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int CourseId { get; set; }
        public CourseEntity Course { get; set; }
        public ICollection<LessonEntity>? Lessons { get; set; }
    }
}
