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
    internal class RoomBedConfiguration : IEntityTypeConfiguration<RoomBed>
    {
        public void Configure(EntityTypeBuilder<RoomBed> builder)
        {
            builder.ToTable("room_beds");
            builder.Property(x => x.BedTypeId).IsRequired();
            builder.Property(x => x.RoomId).IsRequired();
        }
    }
}
