using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Entities
{
    public class Notifycation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int LectureId { get; set; }
        public Lecture Lecture { get; set; }
    }
}
