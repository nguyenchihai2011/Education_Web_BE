using EducationAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationAPI.Configurations
{
    public class StudentQuizConfiguration : IEntityTypeConfiguration<StudentQuizEntity>
    {
        public void Configure(EntityTypeBuilder<StudentQuizEntity> builder)
        {
            builder
                .HasKey(e => new { e.StudentId, e.QuizId });
        }
    }
}
