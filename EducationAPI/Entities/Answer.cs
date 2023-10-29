using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Entities
{

    public class Answer
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public bool IsCorrect { get; set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}
