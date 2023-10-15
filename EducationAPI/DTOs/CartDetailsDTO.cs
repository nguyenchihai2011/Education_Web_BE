using EducationAPI.Entities;
using EducationAPI.Models;

namespace EducationAPI.DTOs
{
    public class CartDetailsDTO
    {
        public int CartId { get; set; }
        public CartEntity? Cart { get; set; }
        public int CourseId { get; set; }
        public CourseEntity? Course { get; set; }
    }
}
