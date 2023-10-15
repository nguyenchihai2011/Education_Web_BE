using EducationAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationAPI.Configurations
{
    public class CartDetailsConfiguration : IEntityTypeConfiguration<CartDetailsEntity>
    {
        public void Configure(EntityTypeBuilder<CartDetailsEntity> builder)
        {
            builder
                .HasKey(e => new { e.CartId, e.CourseId });
        }
    }
}
