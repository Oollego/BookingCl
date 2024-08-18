using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.Configurations
{
    internal class HotelComfortIconTypeConfiguration : IEntityTypeConfiguration<HotelComfortIconType>
    {
        public void Configure(EntityTypeBuilder<HotelComfortIconType> builder)
        {
            builder.ToTable("hotel_comfort_icon_types");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.ComfortName).HasMaxLength(254);
            builder.Property(x => x.ComfortIcon).HasMaxLength(254).IsRequired();

            builder.HasMany(x => x.Hotels)
              .WithMany(x => x.HotelComfortIcons)
              .UsingEntity<HotelComfortIcon>(
              l => l.HasOne<Hotel>().WithMany().HasForeignKey(x => x.HotelId),
              l => l.HasOne<HotelComfortIconType>().WithMany().HasForeignKey(x => x.HotelComfortIconTypeId)
              );
            builder.HasData( new List<HotelComfortIconType>()
            {
                new HotelComfortIconType()
                {
                    Id = 1,
                    ComfortIcon = "crown.png",
                    ComfortName = "popular"
                },
                new HotelComfortIconType()
                {
                    Id = 2,
                    ComfortIcon = "center.png",
                    ComfortName = "city center"
                },
                new HotelComfortIconType()
                {
                    Id = 3,
                    ComfortIcon = "coffee.png",
                    ComfortName = "comfortable"
                }
            });
        }
    }
}