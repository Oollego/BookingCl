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
    internal class NearPlaceNameConfiguration : IEntityTypeConfiguration<NearPlaceName>
    {
        public void Configure(EntityTypeBuilder<NearPlaceName> builder)
        {
            builder.ToTable("near_place_names");
            builder.Property(x => x.Name).IsRequired().HasMaxLength(254);
            builder.Property(x => x.Icon).HasMaxLength(254);

            builder.HasMany<NearPlace>(x => x.NearPlaces)
             .WithOne(x => x.NearPlaceName)
             .HasForeignKey(x => x.NearPlaceNameId)
             .HasPrincipalKey(x => x.Id);
            builder.HasData(new List<NearPlaceName>()
            {
                new NearPlaceName()
                {
                    Id = 1,
                    Name = "airport"
                },
                new NearPlaceName()
                {
                    Id = 2,
                    Name = "railway station"
                },
                new NearPlaceName()
                {
                    Id = 3,
                    Name = "bus station"
                }
            });
        }
    }
}
