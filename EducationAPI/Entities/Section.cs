using EducationAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Entities
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int Index { get; set; }
        public ICollection<Lesson>? Lessons { get; set; }
    }
}
