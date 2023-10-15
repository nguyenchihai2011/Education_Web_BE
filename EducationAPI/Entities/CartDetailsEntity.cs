using EducationAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Entities
{
    [Table("CartDetails")]
    public class CartDetailsEntity
    {
        public int CartId { get; set; }
        public CartEntity Cart { get; set; }
        public int CourseId { get; set; }
        public CourseEntity Course { get; set; }
    }
}
