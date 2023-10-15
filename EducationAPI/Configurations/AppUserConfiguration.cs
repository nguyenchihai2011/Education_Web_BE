using EducationAPI.Entities;
using EducationAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace EducationAPI.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUserEntity>
    {
        public void Configure(EntityTypeBuilder<AppUserEntity> builder)
        {
            builder
             .HasOne(c => c.Student)
             .WithOne(e => e.User)
             .HasForeignKey<StudentEntity>(e => e.UserId)
             .IsRequired();

            builder
             .HasOne(c => c.Lecture)
             .WithOne(e => e.User)
             .HasForeignKey<LectureEntity>(e => e.UserId)
             .IsRequired();

            builder
             .HasOne(c => c.Admin)
             .WithOne(e => e.User)
             .HasForeignKey<AdminEntity>(e => e.UserId)
             .IsRequired();

            builder
                .HasMany(e => e.Comments)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
