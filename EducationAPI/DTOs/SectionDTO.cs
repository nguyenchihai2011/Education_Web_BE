using EducationAPI.Models;

namespace EducationAPI.DTOs
{
    public class SectionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int CourseId { get; set; }
        public ICollection<Lesson>? Lessons { get; set; }
    }
}
