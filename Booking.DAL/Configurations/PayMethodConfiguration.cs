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
    internal class PayMethodConfiguration : IEntityTypeConfiguration<PayMethod>
    {
        public void Configure(EntityTypeBuilder<PayMethod> builder)
        {
            builder.ToTable("pay_methods");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.CardNumber).HasMaxLength(15).IsRequired();
            builder.Property(x => x.CardDate).IsRequired();
            builder.Property(x => x.CardTypeId).IsRequired();

            builder.HasMany<UserProfile>(x => x.UserProfiles)
                .WithOne(x => x.PayMethod)
                .HasForeignKey(x => x.PayMethodId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}
