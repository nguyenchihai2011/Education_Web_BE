using EducationAPI.Entities;
using EducationAPI.Models;

namespace EducationAPI.DTOs
{
    public class QuizDTO
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public int Index { get; set; }
        public string CreateAt { get; set; }
        public string UpdateAt { get; set; }
        public int LessonId { get; set; }
    }
}
