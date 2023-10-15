﻿using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Entities
{
    [Table("Notifycation")]
    public class NotifycationEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateAt { get; set; }
        public int StudentId { get; set; }
        public StudentEntity Student { get; set; }
        public int LectureId { get; set; }
        public LectureEntity Lecture { get; set; }
    }
}