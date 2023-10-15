using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Models
{
    [Table("Response")]
    public class ResponseEntity
    {
        public string? Status { get; set; }
        public string? Message { get; set; } 
    }
}
