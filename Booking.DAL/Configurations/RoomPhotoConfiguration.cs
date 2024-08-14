using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Configurations
{
    internal class RoomPhotoConfiguration: IEntityTypeConfiguration<RoomPhoto>
    {
        public void Configure(EntityTypeBuilder<RoomPhoto> builder)
        {
            builder.ToTable("room_photos");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.PhotoName).IsRequired().HasMaxLength(254);
            builder.Property(x => x.RoomId).IsRequired();
        }
    }
}
