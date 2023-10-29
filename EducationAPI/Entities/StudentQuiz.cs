using System.ComponentModel.DataAnnotations.Schema;
namespace EducationAPI.Entities
{
    public class StudentQuiz
    {
        public bool IsComplete { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}
