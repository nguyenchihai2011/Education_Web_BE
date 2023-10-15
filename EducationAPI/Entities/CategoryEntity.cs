using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Models
{
    [Table("Category")]
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CourseEntity>? Courses { get; set; }
    }
}
