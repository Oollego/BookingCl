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
    internal class NearPlacesConfiguration : IEntityTypeConfiguration<NearPlace>
    {
        public void Configure(EntityTypeBuilder<NearPlace> builder)
        {
            builder.ToTable("near_places");
            builder.Property(x => x.PlaceName).IsRequired().HasMaxLength(254);
            builder.Property(x => x.Distance).IsRequired();
            builder.Property(x => x.DistanceMetric).HasDefaultValue(false);
        }
    }
}
