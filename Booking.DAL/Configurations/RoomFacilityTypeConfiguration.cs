using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Configurations
{
    internal class RoomFacilityTypeConfiguration: IEntityTypeConfiguration<RoomFacilityType>
    {
        public void Configure(EntityTypeBuilder<RoomFacilityType> builder)
        {
            builder.ToTable("room_facility_types");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.RoomFacilityName).IsRequired().HasMaxLength(254);
            builder.Property(x => x.RoomFacilityIcon).HasMaxLength(254).HasDefaultValue("");

            builder.HasMany(x => x.Rooms)
           .WithMany(x => x.RoomFacilityTypes)
           .UsingEntity<RoomFacility>(
           l => l.HasOne<Room>().WithMany().HasForeignKey(x => x.RoomId),
           l => l.HasOne<RoomFacilityType>().WithMany().HasForeignKey(x => x.FacilityTypeId)
           );
        }
    }
}
