using EducationAPI.Entities;

namespace EducationAPI.DTOs
{
    public class CartDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public ICollection<CartDetails>? CartDetails { get; set; }
    }
}
