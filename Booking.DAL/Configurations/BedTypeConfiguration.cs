using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Booking.DAL.Configurations
{
    internal class BedTypeConfiguration: IEntityTypeConfiguration<BedType>
    {
        public void Configure(EntityTypeBuilder<BedType> builder)
        {
            builder.ToTable("bed_types");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.BedTypeName).IsRequired().HasMaxLength(254);
           
            builder.HasMany(x => x.Rooms)
           .WithMany(x => x.BedTypes)
           .UsingEntity<RoomBed>(
           l => l.HasOne<Room>().WithMany().HasForeignKey(x => x.RoomId),
           l => l.HasOne<BedType>().WithMany().HasForeignKey(x => x.BedTypeId)
           );

            builder.HasData(new List<BedType>()
            {
                 new BedType()
                {
                    Id = 1,
                    BedTypeName = "twin-sized bed"
                },
                  new BedType()
                {
                    Id = 2,
                    BedTypeName = "double-sized bed"
                },
                new BedType()
                {
                    Id = 3,
                    BedTypeName = "queen-sized bed"
                },
                 new BedType()
                {
                    Id = 4,
                    BedTypeName = "king-sized bed"
                },
            });
        }
    }
}
