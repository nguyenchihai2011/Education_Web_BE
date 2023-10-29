namespace EducationAPI.DTOs
{
    public class StudentLessonsDTO
    {
        public int StudentId { get; set; }
        public int LessonId { get; set; }
        public bool IsLock { get; set; }
    }
}
