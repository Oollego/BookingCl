using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Booking.Domain.Entity;

namespace Booking.DAL.Configurations
{
    internal class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("cities");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CityName).HasMaxLength(254).IsRequired();
            builder.Property(x => x.CountryId).IsRequired();

            builder.HasMany<UserProfile>(x => x.UserProfiles)
                .WithOne(x => x.City)
                .HasForeignKey(x => x.CityId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}
