using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Entities
{
    [Table("Answer")]
    public class AnswerEntity
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public bool IsCorrect { get; set; }
        public int QuizId { get; set; }
        public QuizEntity Quiz { get; set; }
    }
}
