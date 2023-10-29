using EducationAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationAPI.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder
                .HasMany(e => e.CartDetails)
                .WithOne(c => c.Cart)
                .HasForeignKey(c => c.CartId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
