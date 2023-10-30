using EducationAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationAPI.Configurations
{
    public class CourseConfigugation : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder
                .HasMany(e => e.Comments)
                .WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(e => e.Ratings)
                .WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);


            builder
                .HasMany(e => e.CartDetails)
                .WithOne(c => c.Course)
                .HasForeignKey(c => c.CourseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasMany(e => e.OrderDetails)
                .WithOne(o => o.Course)
                .HasForeignKey(c => c.CourseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);


            builder
                .HasMany(e => e.Sections)
                .WithOne(s => s.Course)
                .HasForeignKey(s => s.CourseId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
