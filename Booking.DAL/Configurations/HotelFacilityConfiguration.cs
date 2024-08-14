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
    internal class HotelFacilityConfiguration : IEntityTypeConfiguration<HotelFacility>
    {
        public void Configure(EntityTypeBuilder<HotelFacility> builder)
        {
            builder.ToTable("hotel_facilities");
            builder.Property(x => x.FacilityId).IsRequired();
            builder.Property(x => x.HotelId).IsRequired();
        }
    }
}
