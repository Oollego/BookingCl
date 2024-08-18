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
            //builder.HasMany<RoomFacilityType>(x => x.RoomFacilityTypes)
            //    .WithMany(x => x.Rooms)
            //    .UsingEntity(x => x.ToTable("RoomFacilities"));
            builder.HasMany<RoomPhoto>(x => x.RoomPhotos)
                .WithOne(x => x.Room)
                .HasForeignKey(x => x.RoomId)
                .HasPrincipalKey(x => x.Id);
            //builder.HasMany<RoomComfortIconType>(x => x.RoomComfortIcons)
            //    .WithOne(x => x.Room)
            //    .HasForeignKey(x => x.RoomId)
            //    .HasPrincipalKey(x => x.Id);

            builder.HasData(new List<Room>
        {
            // Rooms for Hotel Azure (Id = 1)
            new Room { Id = 1, RoomName = "Single Room", RoomPrice = 120.00M, Logo = "room1.jpg", Cancellation = 15.00M, Guests = 1, HotelId = 1 },
            new Room { Id = 2, RoomName = "Double Room", RoomPrice = 180.00M, Logo = "room2.jpg", Cancellation = 0.00M, Guests = 2, HotelId = 1 },
            new Room { Id = 3, RoomName = "Suite", RoomPrice = 300.00M, Logo = "room3.jpg", Cancellation = 0.00M, Guests = 3, HotelId = 1 },

            // Rooms for Greenwood Hotel (Id = 2)
            new Room { Id = 4, RoomName = "Deluxe Room", RoomPrice = 220.00M, Logo = "room4.jpg", Cancellation = 0.00M, Guests = 2, HotelId = 2 },
            new Room { Id = 5, RoomName = "Family Room", RoomPrice = 250.00M, Logo = "room5.jpg", Cancellation = 0.00M, Guests = 4, HotelId = 2 },

            // Rooms for Urban Central (Id = 3)
            new Room { Id = 6, RoomName = "Standard Room", RoomPrice = 150.00M, Logo = "room6.jpg", Cancellation = 10.00M, Guests = 2, HotelId = 3 },
            new Room { Id = 7, RoomName = "Executive Suite", RoomPrice = 350.00M, Logo = "room7.jpg", Cancellation = 15.00M, Guests = 3, HotelId = 3 },

            // Rooms for Mountain Retreat (Id = 4)
            new Room { Id = 8, RoomName = "Mountain View Room", RoomPrice = 200.00M, Logo = "room8.jpg", Cancellation = 20.00M, Guests = 2, HotelId = 4 },
            new Room { Id = 9, RoomName = "Ski Suite", RoomPrice = 280.00M, Logo = "room9.jpg", Cancellation = 0.00M, Guests = 3, HotelId = 4 },

            // Rooms for Coastal Escape (Id = 5)
            new Room { Id = 10, RoomName = "Oceanfront Room", RoomPrice = 240.00M, Logo = "room10.jpg", Cancellation = 0.00M, Guests = 2, HotelId = 5 },
            new Room { Id = 11, RoomName = "Luxury Suite", RoomPrice = 350.00M, Logo = "room11.jpg", Cancellation = 0.00M, Guests = 4, HotelId = 5 },

            // Rooms for Hotel Royal (Id = 6)
            new Room { Id = 12, RoomName = "Classic Room", RoomPrice = 130.00M, Logo = "room12.jpg", Cancellation = 15.00M, Guests = 1, HotelId = 6 },
            new Room { Id = 13, RoomName = "Royal Suite", RoomPrice = 300.00M, Logo = "room13.jpg", Cancellation = 0.00M, Guests = 3, HotelId = 6 },

            // Rooms for Riverside Hotel (Id = 7)
            new Room { Id = 14, RoomName = "River View Room", RoomPrice = 160.00M, Logo = "room14.jpg", Cancellation = 10.00M, Guests = 2, HotelId = 7 },
            new Room { Id = 15, RoomName = "Deluxe River Suite", RoomPrice = 270.00M, Logo = "room15.jpg", Cancellation = 0.00M, Guests = 3, HotelId = 7 },

            // Rooms for Central Park Hotel (Id = 8)
            new Room { Id = 16, RoomName = "Park View Room", RoomPrice = 190.00M, Logo = "room16.jpg", Cancellation = 0.00M, Guests = 2, HotelId = 8 },
            new Room { Id = 17, RoomName = "Luxury Park Suite", RoomPrice = 320.00M, Logo = "room17.jpg", Cancellation = 0.00M, Guests = 4, HotelId = 8 },

            // Rooms for Seaside Hotel (Id = 9)
            new Room { Id = 18, RoomName = "Beachfront Room", RoomPrice = 200.00M, Logo = "room18.jpg", Cancellation = 0.00M, Guests = 2, HotelId = 9 },
            new Room { Id = 19, RoomName = "Seaside Suite", RoomPrice = 300.00M, Logo = "room19.jpg", Cancellation = 0.00M, Guests = 3, HotelId = 9 },

            // Rooms for The Grand Hotel (Id = 10)
            new Room { Id = 20, RoomName = "Grand Room", RoomPrice = 250.00M, Logo = "room20.jpg", Cancellation = 0.00M, Guests = 2, HotelId = 10 },
            new Room { Id = 21, RoomName = "Historic Suite", RoomPrice = 400.00M, Logo = "room21.jpg", Cancellation = 0.00M, Guests = 3, HotelId = 10 },

            // Rooms for City Palace (Id = 11)
            new Room { Id = 22, RoomName = "City View Room", RoomPrice = 140.00M, Logo = "room22.jpg", Cancellation = 0.00M, Guests = 2, HotelId = 11 },
            new Room { Id = 23, RoomName = "Palace Suite", RoomPrice = 290.00M, Logo = "room23.jpg", Cancellation = 25.00M, Guests = 3, HotelId = 11 },

            // Rooms for Boutique Hotel Madrid (Id = 12)
            new Room { Id = 24, RoomName = "Boutique Room", RoomPrice = 180.00M, Logo = "room24.jpg", Cancellation = 14.00M, Guests = 2, HotelId = 12 },
            new Room { Id = 25, RoomName = "Gran Via Suite", RoomPrice = 300.00M, Logo = "room25.jpg", Cancellation = 0.00M, Guests = 3, HotelId = 12 },

            // Rooms for Lakeview Inn (Id = 13)
            new Room { Id = 26, RoomName = "Lakeview Room", RoomPrice = 170.00M, Logo = "room26.jpg", Cancellation = 0.00M, Guests = 2, HotelId = 13 },
            new Room { Id = 27, RoomName = "Lakefront Suite", RoomPrice = 290.00M, Logo = "room27.jpg", Cancellation = 15.00M, Guests = 3, HotelId = 13 },

            // Rooms for Ocean Breeze Hotel (Id = 14)
            new Room { Id = 28, RoomName = "Ocean Breeze Room", RoomPrice = 200.00M, Logo = "room28.jpg", Cancellation = 0.00M, Guests = 2, HotelId = 14 },
            new Room { Id = 29, RoomName = "Business Suite", RoomPrice = 320.00M, Logo = "room29.jpg", Cancellation = 0.00M, Guests = 3, HotelId = 14 },

            // Rooms for Countryside Inn (Id = 15)
            new Room { Id = 30, RoomName = "Countryside Room", RoomPrice = 150.00M, Logo = "room30.jpg", Cancellation = 0.00M, Guests = 2, HotelId = 15 },
            new Room { Id = 31, RoomName = "Rustic Suite", RoomPrice = 260.00M, Logo = "room31.jpg", Cancellation = 0.00M, Guests = 3, HotelId = 15 },
        });
        }
    }
}
 
