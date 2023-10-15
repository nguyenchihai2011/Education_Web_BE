﻿using EducationAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace EducationAPI.Data
{
    public class LessonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string VideoUrl { get; set; }
        public DateTime Time { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int SectionId { get; set; }
        public SectionEntity? Section { get; set; }
        public ICollection<QuizEntity>? Quizzes { get; set; }
        public ICollection<StudentLessonEntity>? StudentLessons { get; set; }
    }
}