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
    internal class HotelInfoCellConfiguration : IEntityTypeConfiguration<HotelInfoCell>
    {
        public void Configure(EntityTypeBuilder<HotelInfoCell> builder)
        {
            builder.ToTable("hotel_info_cells");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.TextLine_1).HasMaxLength(254).IsRequired();
            builder.Property(x => x.TextLine_2).HasMaxLength(254).HasDefaultValue("");
            builder.Property(x => x.InfoIcon).HasMaxLength(254).HasDefaultValue("");
            builder.Property(x => x.HotelId).IsRequired();
        }
    }
}
