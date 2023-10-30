using EducationAPI.Entities;
using EducationAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace EducationAPI.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder
             .HasOne(c => c.Student)
             .WithOne(e => e.AppUser)
             .HasForeignKey<Student>(e => e.UserId)
             .IsRequired();

            builder
             .HasOne(c => c.Lecture)
             .WithOne(e => e.AppUser)
             .HasForeignKey<Lecture>(e => e.UserId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);


            builder
             .HasOne(c => c.Admin)
             .WithOne(e => e.AppUser)
             .HasForeignKey<Admin>(e => e.UserId)
             .IsRequired();

            builder
                .HasMany(e => e.Comments)
                .WithOne(e => e.AppUser)
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
