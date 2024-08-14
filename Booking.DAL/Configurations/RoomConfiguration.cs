using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Configurations
{
    internal class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("rooms");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RoomName).HasMaxLength(254);
            builder.Property(x => x.Logo).HasMaxLength(254).HasDefaultValue("");
            builder.Property(x => x.Cancellation).HasDefaultValue(0).HasPrecision(10, 2);
            builder.Property(x => x.RoomPrice).HasPrecision(10, 2);
            builder.Property(x => x.HotelId).IsRequired();
           
            builder.HasMany<Book>(x => x.Books)
                .WithOne(x => x.Room)
                .HasForeignKey(x => x.RoomId)
                .HasPrincipalKey(x => x.Id);
            //builder.HasMany<BedType>(x => x.BedTypes)
            //    .WithMany(x => x.Rooms)
            //    .UsingEntity(x => x.ToTable("Beds"));
            builder.HasMany<RoomFacilityType>(x => x.RoomFacilityTypes)
                .WithMany(x => x.Rooms)
                .UsingEntity(x => x.ToTable("RoomFacilities"));
            builder.HasMany<RoomPhoto>(x => x.RoomPhotos)
                .WithOne(x => x.Room)
                .HasForeignKey(x => x.RoomId)
                .HasPrincipalKey(x => x.Id);
            //builder.HasMany<RoomComfortIconType>(x => x.RoomComfortIcons)
            //    .WithOne(x => x.Room)
            //    .HasForeignKey(x => x.RoomId)
            //    .HasPrincipalKey(x => x.Id);
        }
    }
}
