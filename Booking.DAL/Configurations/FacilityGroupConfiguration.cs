using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySql.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.Configurations
{
    internal class FacilityGroupConfiguration : IEntityTypeConfiguration<FacilityGroup>
    {
        public void Configure(EntityTypeBuilder<FacilityGroup> builder)
        {
            builder.ToTable("facility_groups");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FacilityGroupName).HasMaxLength(254).IsRequired();
            builder.Property(x => x.FacilityGroupIcon).HasMaxLength(254).HasDefaultValue("");

            builder.HasMany<Facility>(x => x.Facilities)
                .WithOne(x => x.FacilityGroup)
                .HasForeignKey(x => x.FacilityGroupId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}
