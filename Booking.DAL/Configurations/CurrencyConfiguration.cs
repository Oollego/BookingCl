using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Configurations
{
    internal class CurrencyConfiguration: IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("currencies");
            builder.HasKey(x => x.CurrencyCode);
            builder.Property(x => x.CurrencyCode).IsRequired().HasMaxLength(3);
            builder.Property(x => x.CurrencyLetter).IsRequired().HasMaxLength(1);
            builder.Property(x => x.CurrencyName).HasMaxLength(100);

            builder.HasMany<UserProfile>(x => x.UserProfiles)
                .WithOne(x => x.Currency)
                .HasForeignKey(x => x.CurrencyCodeId)
                .HasPrincipalKey(x => x.CurrencyCode);
        }
    }
}
