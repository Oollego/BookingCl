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
    internal class NearPlaceGroupConfiguration : IEntityTypeConfiguration<NearPlaceGroup>
    {
        public void Configure(EntityTypeBuilder<NearPlaceGroup> builder)
        {
            builder.ToTable("near_place_groups");
            builder.Property(x => x.GroupName).IsRequired().HasMaxLength(254);
            builder.Property(x => x.GroupIcon).HasMaxLength(254);

            builder.HasMany<NearPlace>(x => x.NearPlaces)
             .WithOne(x => x.NearPlaceGroup)
             .HasForeignKey(x => x.NearPlaceGroupId)
             .HasPrincipalKey(x => x.id);
        }
    }
}
