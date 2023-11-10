using EducationAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Entities
{
    public class CartDetails
    {
        public int CartId { get; set; }
        public int CourseId { get; set; }
        public Cart? Cart { get; set; }
        public Course? Course { get; set; }
    }
}
