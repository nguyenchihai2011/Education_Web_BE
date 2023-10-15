using EducationAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationAPI.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<AdminEntity>
    {
        public void Configure(EntityTypeBuilder<AdminEntity> builder)
        {
        }
    }
}
