using EducationAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationAPI.Configurations
{
    public class PromotionConfiguration : IEntityTypeConfiguration<PromotionEntity>
    {
        public void Configure(EntityTypeBuilder<PromotionEntity> builder)
        {
            builder
                .HasMany(e => e.Courses)
                .WithOne(c => c.Promotion)
                .HasForeignKey(c => c.PromotionId)
                .IsRequired(false);
        }
    }
}
