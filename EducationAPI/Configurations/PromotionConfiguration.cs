using EducationAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationAPI.Configurations
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder
                .HasMany(e => e.Courses)
                .WithOne(c => c.Promotion)
                .HasForeignKey(c => c.PromotionId)
                .IsRequired(false);
        }
    }
}
