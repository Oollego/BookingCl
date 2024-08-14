using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Booking.Domain.Entity;

namespace Booking.DAL.Configurations
{
    internal class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("countries");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CountryName).HasMaxLength(254).IsRequired();

            builder.HasMany<City>(x => x.Cities)
               .WithOne(x => x.Country)
               .HasForeignKey(x => x.CountryId)
               .HasPrincipalKey(x => x.Id);
        }
    }
}
