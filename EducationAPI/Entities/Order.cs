using EducationAPI.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Payment { get; set; }
        public float TotalPrice { get; set; }
        public DateTime CreateAt { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
