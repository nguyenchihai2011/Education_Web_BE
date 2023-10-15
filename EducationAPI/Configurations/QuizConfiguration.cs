using EducationAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationAPI.Configurations
{
    public class QuizConfiguration : IEntityTypeConfiguration<QuizEntity>
    {
        public void Configure(EntityTypeBuilder<QuizEntity> builder)
        {
            builder
                .HasMany(e => e.StudentQuizzes)
                .WithOne(s => s.Quiz)
                .HasForeignKey(s => s.QuizId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);


            builder
                .HasMany(e => e.Answers)
                .WithOne(a => a.Quiz)
                .HasForeignKey(a => a.QuizId)
                .IsRequired();
        }
    }
}
