using EducationAPI.Data;
using EducationAPI.Entities;

namespace EducationAPI.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Payment { get; set; }
        public float TotalPrice { get; set; }
        public DateTime CreateAt { get; set; }
        public int StudentId { get; set; }
        public ICollection<OrderDetailsDTO> OrderDetailsDTO { get; set; } 

    }
}
