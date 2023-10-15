using EducationAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Entities
{
    [Table("StudentLesson")]
    public class StudentLessonEntity
    {
        public bool isComplete { get; set; }
        public bool IsLock { get; set; }
        public int StudentId { get; set; }
        public StudentEntity Student { get; set; }
        public int LessonId { get; set; }
        public LessonEntity Lesson { get; set; }
    }
}
