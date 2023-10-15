using EducationAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationAPI.Configurations
{
    public class SectionConfiguration : IEntityTypeConfiguration<SectionEntity>
    {
        public void Configure(EntityTypeBuilder<SectionEntity> builder)
        {
            builder
                .HasMany(e => e.Lessons)
                .WithOne(l => l.Section)
                .HasForeignKey(l => l.SectionId)
                .IsRequired();
        }
    }
}
