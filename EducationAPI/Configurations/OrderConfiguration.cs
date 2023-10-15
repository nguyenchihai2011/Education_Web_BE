﻿using EducationAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducationAPI.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder
                .HasMany(e => e.OrderDetails)
                .WithOne(o => o.Order)
                .HasForeignKey(o => o.OrderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
