using EducationAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationAPI.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<RatingEntity>
    {
        public void Configure(EntityTypeBuilder<RatingEntity> builder)
        {
        }
    }
}
