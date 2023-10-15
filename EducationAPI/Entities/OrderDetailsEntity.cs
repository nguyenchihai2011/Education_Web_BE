using EducationAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Entities
{
    [Table("OrderDetails")]
    public class OrderDetailsEntity
    {
        public float Price { get; set; }
        public int OrderId { get; set; }
        public OrderEntity Order { get; set; }
        public int CourseId { get; set; }
        public CourseEntity Course { get; set; }
    }
}
