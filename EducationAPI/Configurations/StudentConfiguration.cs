using EducationAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace EducationAPI.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder
                .HasMany(e => e.Orders)
                .WithOne(c => c.Student)
                .HasForeignKey(c => c.StudentId)
                .IsRequired();


            builder
                .HasMany(e => e.Notifycations)
                .WithOne(c => c.Student)
                .HasForeignKey(c => c.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            

            builder
                .HasMany(e => e.Ratings)
                .WithOne(c => c.Student)
                .HasForeignKey(c => c.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);


            builder
                .HasOne(e => e.Cart)
                .WithOne(c => c.Student)
                .HasForeignKey<Cart>(c => c.StudentId)
                .IsRequired();

            builder
                .HasMany(e => e.StudentQuizzes)
                .WithOne(s => s.Student)
                .HasForeignKey(s => s.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);


            builder
                .HasMany(e => e.StudentLessons)
                .WithOne(s => s.Student)
                .HasForeignKey(s => s.StudentId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
