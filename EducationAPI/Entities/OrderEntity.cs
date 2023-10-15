using EducationAPI.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Entities
{
    [Table("Order")]
    public class OrderEntity
    {
        public int Id { get; set; }
        public Payment Payment { get; set; }
        public DateTime CreateAt { get; set; }
        public int StudentId { get; set; }
        public StudentEntity Student { get; set; }
        public ICollection<OrderDetailsEntity> OrderDetails { get; set; }
    }
}
