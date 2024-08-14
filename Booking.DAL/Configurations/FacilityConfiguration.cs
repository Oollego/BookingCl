using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.Configurations
{
    internal class FacilityConfiguration : IEntityTypeConfiguration<Facility>
    {
        public void Configure(EntityTypeBuilder<Facility> builder)
        {
            builder.ToTable("facilities");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FacilityName).HasMaxLength(254).IsRequired();
            builder.Property(x => x.FacilityGroupId).IsRequired();
          
        }
    }
}
