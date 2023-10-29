using EducationAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Entities
{
    public class Lecture
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string AvatarUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailPaypal { get; set; }
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<Notifycation>? Notifycations { get; set; }
        public ICollection<Course>? Courses { get; set; }
        public ICollection<Promotion>? Promotions { get; set; }
    }
}
