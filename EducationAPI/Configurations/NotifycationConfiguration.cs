using EducationAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationAPI.Configurations
{
    public class NotifycationConfiguration : IEntityTypeConfiguration<Notifycation>
    {
        public void Configure(EntityTypeBuilder<Notifycation> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.StudentId).IsRequired();

            builder.Property(e => e.LectureId).IsRequired();
                
        }
    }
}
