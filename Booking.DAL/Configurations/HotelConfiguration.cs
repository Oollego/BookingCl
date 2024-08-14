using Microsoft.EntityFrameworkCore;
using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Configurations
{
    internal class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.ToTable("Hotels");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.HotelName).HasMaxLength(254).IsRequired();
            builder.Property(x => x.HotelAddress).HasMaxLength(254).IsRequired();
            builder.Property(x => x.HotelPhone).HasMaxLength(25).HasDefaultValue("");
            builder.Property(x => x.HotelPhoto).HasMaxLength(254).HasDefaultValue("");
            builder.Property(x => x.Description).HasMaxLength(3000).HasDefaultValue("");
            builder.Property(x => x.CityGuide).HasMaxLength(1200).HasDefaultValue("");
            builder.Property(x => x.FixedDays).HasDefaultValue(0);
            builder.Property(x => x.IsPet).HasDefaultValue(false);

            builder.HasMany<Room>(x => x.Rooms)
                .WithOne(x => x.Hotel)
                .HasForeignKey(x => x.HotelId)
                .HasPrincipalKey(x => x.Id);
            builder.HasMany<HotelInfoCell>(x => x.HotelInfoCells)
                .WithOne(x => x.Hotel)
                .HasForeignKey(x => x.HotelId)
                .HasPrincipalKey(x => x.Id);
            builder.HasMany<NearPlace>(x => x.NearPlaces)
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
        }
    }
}
