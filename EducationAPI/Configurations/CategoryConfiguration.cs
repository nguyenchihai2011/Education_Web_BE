using EducationAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationAPI.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder
                .HasMany(e => e.Courses)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId)
                .IsRequired();
                
        }
    }
}
