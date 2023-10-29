using EducationAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Entities
{
    public class OrderDetails
    {
        public float Price { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
