using EducationAPI.Entities;

namespace EducationAPI.DTOs
{
    public class AnswerDTO
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public bool IsCorrect { get; set; }
        public int QuizId { get; set; }
    }
}
