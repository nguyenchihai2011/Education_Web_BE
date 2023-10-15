using EducationAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationAPI.Configurations
{
    public class StudentLessonConfiguration : IEntityTypeConfiguration<StudentLessonEntity>
    {
        public void Configure(EntityTypeBuilder<StudentLessonEntity> builder)
        {
            builder
                .HasKey(e => new { e.StudentId, e.LessonId });
        }
    }
}
