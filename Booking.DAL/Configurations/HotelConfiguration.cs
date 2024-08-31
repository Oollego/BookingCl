﻿using Microsoft.EntityFrameworkCore;
using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Booking.DAL.Configurations
{
    internal class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.ToTable("Hotels");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.HotelName).HasMaxLength(254).IsRequired();
            builder.Property(x => x.HotelAddress).HasMaxLength(254).IsRequired();
            builder.Property(x => x.HotelPhone).HasMaxLength(25).HasDefaultValue("");
            builder.Property(x => x.HotelImage).HasMaxLength(254).HasDefaultValue("");
            builder.Property(x => x.Description).HasMaxLength(3000).HasDefaultValue("");
            builder.Property(x => x.CityGuide).HasMaxLength(1200).HasDefaultValue("");
            builder.Property(x => x.Stars).HasDefaultValue(0);
            builder.Property(x => x.FixedDays).HasDefaultValue(0);
            builder.Property(x => x.IsPet).HasDefaultValue(false);
            builder.Property(x => x.CityId).IsRequired();
            builder.ToTable(t => t.HasCheckConstraint("Stars", "Stars >= 0 AND Stars <= 5"));

            builder.HasMany<Room>(x => x.Rooms)
                .WithOne(x => x.Hotel)
                .HasForeignKey(x => x.HotelId)
                .HasPrincipalKey(x => x.Id);
            builder.HasMany<HotelInfoCell>(x => x.HotelInfoCells)
                .WithOne(x => x.Hotel)
                .HasForeignKey(x => x.HotelId)
                .HasPrincipalKey(x => x.Id);
            builder.HasMany<NearStation>(x => x.NearStations)
                .WithOne(x => x.Hotel)
                .HasForeignKey(x => x.HotelId)
                .HasPrincipalKey(x => x.Id);
            //builder.HasMany<Facility>(x => x.Facilities)
            //    .WithMany(x => x.Hotels)
            //    .UsingEntity("HotelFacilities");
            builder.HasMany<Review>(x => x.Reviews)
                .WithOne(x => x.Hotel)
                .HasForeignKey(x => x.HotelId)
                .HasPrincipalKey(x => x.Id);
            builder.HasMany(x => x.Facilities)
              .WithMany(x => x.Hotels)
              .UsingEntity<HotelFacility>(
              l => l.HasOne<Facility>().WithMany().HasForeignKey(x => x.FacilityId),
              l => l.HasOne<Hotel>().WithMany().HasForeignKey(x => x.HotelId)
            );


            builder.HasData(new List<Hotel>() {
                new Hotel
                {
                    Id = 1,
                    HotelName = "Hotel Azure",
                    HotelAddress = "1 Seaside Rd, Barcelona, Spain",
                    HotelPhone = "+34 931 123 456",
                    HotelImage = "32525685.webp",
                    CityGuide = "Located on the beautiful Barcelona beachfront.",
                    Description = "Hotel Azure offers comfort with a view of the Mediterranean Sea.",
                    Stars = 4,
                    FixedDays = 5,
                    IsPet = true,
                    CityId = 197
                },
                new Hotel
                {
                    Id = 2,
                    HotelName = "Greenwood Hotel",
                    HotelAddress = "12 Forest Lane, Madrid, Spain",
                    HotelPhone = "+34 915 987 654",
                    HotelImage = "66611711.webp",
                    CityGuide = "A peaceful retreat surrounded by the lush parks of Madrid.",
                    Description = "Greenwood Hotel is a perfect escape to nature in Spain's capital.",
                    Stars = 5,
                    FixedDays = 3,
                    IsPet = false,
                    CityId = 196
                },
                new Hotel
                {
                    Id = 3,
                    HotelName = "Urban Central",
                    HotelAddress = "10 Friedrichstrasse, Berlin, Germany",
                    HotelPhone = "+49 30 789 0123",
                    HotelImage = "395641436.webp",
                    CityGuide = "Located in the vibrant heart of Berlin, ideal for business and leisure.",
                    Description = "Urban Central combines luxury with the bustling energy of Berlin.",
                    Stars = 5,
                    FixedDays = 2,
                    IsPet = false,
                    CityId = 81
                },
                new Hotel
                {
                    Id = 4,
                    HotelName = "Mountain Retreat",
                    HotelAddress = "55 Alpine Rd, Hamburg, Germany",
                    HotelPhone = "+49 8821 123 456",
                    HotelImage = "561822462.webp",
                    CityGuide = "Nestled in the Bavarian Alps, perfect for ski enthusiasts.",
                    Description = "Mountain Retreat offers stunning views of the Zugspitze.",
                    Stars = 4,
                    FixedDays = 4,
                    IsPet = true,
                    CityId = 84
                },
                new Hotel
                {
                    Id = 5,
                    HotelName = "Coastal Escape",
                    HotelAddress = "9 Promenade des Anglais, Nice, Odessa",
                    HotelPhone = "+34 4 93 123 456",
                    HotelImage = "183805356.webp",
                    CityGuide = "Overlooking the Mediterranean, perfect for a seaside holiday.",
                    Description = "Coastal Escape provides luxury on the famous French Riviera.",
                    Stars = 4,
                    FixedDays = 7,
                    IsPet = true,
                    CityId = 218
                },
                new Hotel
                {
                    Id = 6,
                    HotelName = "Hotel Royal",
                    HotelAddress = "22 Maidan Nezalezhnosti, Kyiv, Ukraine",
                    HotelPhone = "+380 44 123 4567",
                    HotelImage = "245015962.webp",
                    CityGuide = "Located in the heart of Kyiv, overlooking Independence Square.",
                    Description = "Hotel Royal combines modern luxury with Ukrainian hospitality.",
                    Stars = 5,
                    FixedDays = 6,
                    IsPet = true,
                    CityId = 216
                },
                new Hotel
                {
                    Id = 7,
                    HotelName = "Riverside Hotel",
                    HotelAddress = "18 Dnipro Embankment, Dnipro, Ukraine",
                    HotelPhone = "+380 56 987 6543",
                    HotelImage = "548107817.webp",
                    CityGuide = "Situated along the Dnipro River, offering serene views.",
                    Description = "Riverside Hotel is a tranquil escape in the city of Dnipro.",
                    Stars = 4,
                    FixedDays = 5,
                    IsPet = false,
                    CityId = 220
                },
                new Hotel
                {
                    Id = 8,
                    HotelName = "Central Park Hotel",
                    HotelAddress = "15 Khreshchatyk St, Kyiv, Ukraine",
                    HotelPhone = "+380 44 789 0123",
                    HotelImage = "315505921.webp",
                    CityGuide = "In the center of Kyiv, perfect for business and leisure.",
                    Description = "Central Park Hotel offers luxury and comfort in the capital.",
                    Stars = 5,
                    FixedDays = 3,
                    IsPet = true,
                    CityId = 216
                },
                new Hotel
                {
                    Id = 9,
                    HotelName = "Seaside Hotel",
                    HotelAddress = "3 Playa del Postiguet, Valencia, Spain",
                    HotelPhone = "+34 965 123 789",
                    HotelImage = "567615599.webp",
                    CityGuide = "Located on the beautiful beaches of Alicante.",
                    Description = "Seaside Hotel offers breathtaking views of the Mediterranean.",
                    Stars = 4,
                    FixedDays = 6,
                    IsPet = false,
                    CityId = 199
                },
                new Hotel
                {
                    Id = 10,
                    HotelName = "The Grand Hotel",
                    HotelAddress = "1 Unter den Linden, Berlin, Germany",
                    HotelPhone = "+49 30 987 6543",
                    HotelImage = "451889310.webp",
                    CityGuide = "A historic hotel located on the iconic Unter den Linden boulevard.",
                    Description = "The Grand Hotel blends historic charm with modern luxury.",
                    Stars = 5,
                    FixedDays = 4,
                    IsPet = true,
                    CityId = 81
                },
                new Hotel
                {
                    Id = 11,
                    HotelName = "City Palace",
                    HotelAddress = "7 Andriyivskyy Descent, Kyiv, Ukraine",
                    HotelPhone = "+380 44 555 7890",
                    HotelImage = "250530652.webp",
                    CityGuide = "Situated near the historic Andriyivskyy Descent.",
                    Description = "City Palace offers luxury accommodation in a cultural setting.",
                    Stars = 5,
                    FixedDays = 2,
                    IsPet = true,
                    CityId = 216
                },
                new Hotel
                {
                    Id = 12,
                    HotelName = "Boutique Hotel Madrid",
                    HotelAddress = "33 Gran Via, Madrid, Spain",
                    HotelPhone = "+34 915 567 890",
                    HotelImage = "212052051.webp",
                    CityGuide = "Located on the vibrant Gran Via, perfect for shopping and dining.",
                    Description = "Boutique Hotel Madrid combines style with central convenience.",
                    Stars = 4,
                    FixedDays = 5,
                    IsPet = false,
                    CityId = 196
                },
                new Hotel
                {
                    Id = 13,
                    HotelName = "Lakeview Inn",
                    HotelAddress = "44 Am Stadtpark, Hamburg, Germany",
                    HotelPhone = "+49 40 123 4567",
                    HotelImage = "264682690.webp",
                    CityGuide = "Overlooking the Alster Lake, offering serene views in Hamburg.",
                    Description = "Lakeview Inn is a peaceful retreat in the heart of Hamburg.",
                    Stars = 4,
                    FixedDays = 6,
                    IsPet = true,
                    CityId = 84
                },
                new Hotel
                {
                    Id = 14,
                    HotelName = "Ocean Breeze Hotel",
                    HotelAddress = "23 Paseo de la Castellana, Madrid, Spain",
                    HotelPhone = "+34 915 876 543",
                    HotelImage = "494885390.webp",
                    CityGuide = "Located in the business district of Madrid.",
                    Description = "Ocean Breeze Hotel offers modern luxury with excellent amenities.",
                    Stars = 4,
                    FixedDays = 4,
                    IsPet = false,
                    CityId = 196
                },
                new Hotel
                {
                    Id = 15,
                    HotelName = "Countryside Inn",
                    HotelAddress = "88 Rural Rd, Stuttgart, Germany",
                    HotelPhone = "+49 89 654 3210",
                    HotelImage = "105001905.webp",
                    CityGuide = "Nestled in the Bavarian countryside, ideal for a peaceful retreat.",
                    Description = "Countryside Inn offers a rustic experience with modern comforts.",
                    Stars = 3,
                    FixedDays = 7,
                    IsPet = true,
                    CityId = 86
                }

            });
        }
    }
}
