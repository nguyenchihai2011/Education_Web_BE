using EducationAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Entities
{
    public class StudentLesson
    {
        public bool IsLock { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public int LessonId { get; set; }
        public Lesson? Lesson { get; set; }
    }
}
