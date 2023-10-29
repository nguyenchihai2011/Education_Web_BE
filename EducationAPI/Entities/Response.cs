using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Models
{
    public class Response
    {
        public string? Status { get; set; }
        public string? Message { get; set; } 
    }
}
