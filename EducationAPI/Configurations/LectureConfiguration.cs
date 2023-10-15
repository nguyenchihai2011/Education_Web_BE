using EducationAPI.Entities;
using EducationAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationAPI.Configurations
{
    public class LectureConfiguration : IEntityTypeConfiguration<LectureEntity>
    {
        public void Configure(EntityTypeBuilder<LectureEntity> builder)
        {
            builder
                .HasMany(e => e.Notifycations)
                .WithOne(n => n.Lecture)
                .HasForeignKey(n => n.LectureId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(e => e.Courses)
                .WithOne(c => c.Lecture)
                .HasForeignKey(c => c.LectureId)
                .IsRequired();

            builder
                .HasMany(e => e.Promotions)
                .WithOne(p => p.Lecture)
                .HasForeignKey(p => p.LectureId)
                .IsRequired(false);
        }
    }
}
