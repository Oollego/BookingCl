using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Configurations
{
    internal class RoomComfortIconTypeConfiguration : IEntityTypeConfiguration<RoomComfortIconType>
    {
        public void Configure(EntityTypeBuilder<RoomComfortIconType> builder)
        {
            builder.ToTable("room_comfort_icon_types");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.ComfortName).HasMaxLength(254);
            builder.Property(x => x.ComfortIcon).HasMaxLength(254).IsRequired();

            builder.HasMany(x => x.Rooms)
              .WithMany(x => x.RoomComfortIcons)
              .UsingEntity<RoomComfortIcon>(
              l => l.HasOne<Room>().WithMany().HasForeignKey(x => x.RoomId),
              l => l.HasOne<RoomComfortIconType>().WithMany().HasForeignKey(x => x.RoomComfortIconTypeId)
              );
            builder.HasData(new List<RoomComfortIconType>()
            {
                new RoomComfortIconType()
                {
                    Id = 1,
                    ComfortIcon = "wifi.png",
                    ComfortName = "free wi-fi"
                },
                new RoomComfortIconType()
                {
                    Id = 2,    
                    ComfortIcon = "bath.png",
                    ComfortName = "bath"
                },
                new RoomComfortIconType()
                {
                    Id = 3,
                    ComfortIcon = "pool.png",
                    ComfortName = "private pool"
                }
            });
        }
    }
}
