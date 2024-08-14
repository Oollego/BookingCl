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
    internal class RoomComfortIconConfiguration : IEntityTypeConfiguration<RoomComfortIcon>
    {
        public void Configure(EntityTypeBuilder<RoomComfortIcon> builder)
        {
            builder.ToTable("room_comfort_icons");
            builder.Property(x => x.RoomId).IsRequired();
            builder.Property(x => x.RoomComfortIconTypeId).IsRequired();

        }
    }
}
