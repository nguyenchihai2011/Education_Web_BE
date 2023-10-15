﻿using EducationAPI.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Entities
{
    [Table("Admin")]
    public class AdminEntity
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public string? AvatarUrl { get; set; }
        public string? FirstName { get; set; } 
        public string? LastName { get; set; }
        public string UserId { get; set; }
        public AppUserEntity User { get; set; }
    }
}
