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
    internal class RoomFacilityConfiguration : IEntityTypeConfiguration<RoomFacility>
    {
        public void Configure(EntityTypeBuilder<RoomFacility> builder)
        {
            builder.ToTable("room_facilities");
            builder.Property(x => x.FacilityTypeId).IsRequired();
            builder.Property(x => x.RoomId).IsRequired();
        }
    }
}
