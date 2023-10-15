using EducationAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationAPI.Configurations
{
    public class OrderDetailsConfiguration : IEntityTypeConfiguration<OrderDetailsEntity>
    {
        public void Configure(EntityTypeBuilder<OrderDetailsEntity> builder)
        {
            builder
                .HasKey(e => new { e.OrderId, e.CourseId });
        }
    }
}
