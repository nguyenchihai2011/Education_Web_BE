using EducationAPI.Entities;

namespace EducationAPI.DTOs
{
    public class CartDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public StudentEntity? Student { get; set; }
        public ICollection<CartDetailsEntity>? CartDetails { get; set; }
    }
}
