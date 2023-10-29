using EducationAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationAPI.Configurations
{
    public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
    {
        public void Configure(EntityTypeBuilder<Lesson> builder)
        {
            builder
                .HasMany(e => e.StudentLessons)
                .WithOne(s => s.Lesson)
                .HasForeignKey(s => s.LessonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);


            builder
                .HasMany(e => e.Quizzes)
                .WithOne(q => q.Lesson)
                .HasForeignKey(q => q.LessonId)
                .IsRequired();
        }
    }
}
