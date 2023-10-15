using EducationAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Entities
{
    [Table("Cart")]
    public class CartEntity
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public StudentEntity Student { get; set; }
        public ICollection<CartDetailsEntity>? CartDetails { get; set; }
    }
}
