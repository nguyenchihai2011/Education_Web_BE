using System.ComponentModel.DataAnnotations.Schema;
namespace EducationAPI.Entities
{
    [Table("StudentQuiz")]
    public class StudentQuizEntity
    {
        public bool IsComplete { get; set; }
        public int StudentId { get; set; }
        public StudentEntity Student { get; set; }
        public int QuizId { get; set; }
        public QuizEntity Quiz { get; set; }
    }
}
