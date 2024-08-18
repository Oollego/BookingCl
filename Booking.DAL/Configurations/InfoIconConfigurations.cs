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
    internal class InfoIconConfigurations : IEntityTypeConfiguration<InfoIcon>
    {
        public void Configure(EntityTypeBuilder<InfoIcon> builder)
        {
            builder.ToTable("info_icons");
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.IconName).IsRequired().HasMaxLength(254);
            builder.Property(x => x.IconFileName).HasMaxLength(254);

            builder.HasMany<HotelInfoCell>(x => x.HotelInfoCells)
               .WithOne(x => x.InfoIcon)
               .HasForeignKey(x => x.InfoIconId)
               .HasPrincipalKey(x => x.Id);

            builder.HasData(new List<InfoIcon>()
            {
                new InfoIcon()
                {
                    Id = 1,
                    IconName = "clock",
                    IconFileName = "clock.png"
                },
                new InfoIcon()
                {
                    Id = 2,
                    IconName = "bed",
                    IconFileName = "bed.png"
                },
                new InfoIcon()
                {
                    Id = 3,
                    IconName = "center",
                    IconFileName = "center.png"
                },
                new InfoIcon()
                {
                    Id = 4,
                    IconName = "train",
                    IconFileName = "train.png"
                },
                 new InfoIcon()
                {
                    Id = 5,
                    IconName = "pet",
                    IconFileName = "pet.png"
                }
            });
        }
    }
}
