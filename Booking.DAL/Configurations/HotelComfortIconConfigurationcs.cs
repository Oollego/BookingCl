using Booking.Domain.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.Configurations
{
    internal class HotelComfortIconConfigurationcs : IEntityTypeConfiguration<HotelComfortIcon>
    {
        public void Configure(EntityTypeBuilder<HotelComfortIcon> builder)
        {
            builder.ToTable("hotel_comfort_icons");
            builder.Property(x => x.HotelId).IsRequired();
            builder.Property(x => x.HotelComfortIconTypeId).IsRequired();

            builder.HasData(new List<HotelComfortIcon>
            {
                // Hotel Azure (Id = 1) - 3 rows
                new HotelComfortIcon { HotelId = 1, HotelComfortIconTypeId = 1 }, // popular
                new HotelComfortIcon { HotelId = 1, HotelComfortIconTypeId = 2 }, // city center
                new HotelComfortIcon { HotelId = 1, HotelComfortIconTypeId = 3 }, // comfortable

                // Greenwood Hotel (Id = 2) - 2 rows
                new HotelComfortIcon { HotelId = 2, HotelComfortIconTypeId = 1 }, // popular
                new HotelComfortIcon { HotelId = 2, HotelComfortIconTypeId = 3 }, // comfortable

                // Urban Central (Id = 3) - 3 rows
                new HotelComfortIcon { HotelId = 3, HotelComfortIconTypeId = 2 }, // city center
                new HotelComfortIcon { HotelId = 3, HotelComfortIconTypeId = 3 }, // comfortable
                new HotelComfortIcon { HotelId = 3, HotelComfortIconTypeId = 1 }, // popular

                // Mountain Retreat (Id = 4) - 2 rows
                new HotelComfortIcon { HotelId = 4, HotelComfortIconTypeId = 1 }, // popular
                new HotelComfortIcon { HotelId = 4, HotelComfortIconTypeId = 3 }, // comfortable

                // Coastal Escape (Id = 5) - 3 rows
                new HotelComfortIcon { HotelId = 5, HotelComfortIconTypeId = 2 }, // city center
                new HotelComfortIcon { HotelId = 5, HotelComfortIconTypeId = 3 }, // comfortable
                new HotelComfortIcon { HotelId = 5, HotelComfortIconTypeId = 1 }, // popular

                // Hotel Royal (Id = 6) - 2 rows
                new HotelComfortIcon { HotelId = 6, HotelComfortIconTypeId = 1 }, // popular
                new HotelComfortIcon { HotelId = 6, HotelComfortIconTypeId = 3 }, // comfortable

                // Riverside Hotel (Id = 7) - 3 rows
                new HotelComfortIcon { HotelId = 7, HotelComfortIconTypeId = 2 }, // city center
                new HotelComfortIcon { HotelId = 7, HotelComfortIconTypeId = 3 }, // comfortable
                new HotelComfortIcon { HotelId = 7, HotelComfortIconTypeId = 1 }, // popular

                // Central Park Hotel (Id = 8) - 2 rows
                new HotelComfortIcon { HotelId = 8, HotelComfortIconTypeId = 1 }, // popular
                new HotelComfortIcon { HotelId = 8, HotelComfortIconTypeId = 2 }, // city center

                // Seaside Hotel (Id = 9) - 2 rows
                new HotelComfortIcon { HotelId = 9, HotelComfortIconTypeId = 1 }, // popular
                new HotelComfortIcon { HotelId = 9, HotelComfortIconTypeId = 3 }, // comfortable

                // The Grand Hotel (Id = 10) - 3 rows
                new HotelComfortIcon { HotelId = 10, HotelComfortIconTypeId = 1 }, // popular
                new HotelComfortIcon { HotelId = 10, HotelComfortIconTypeId = 2 }, // city center
                new HotelComfortIcon { HotelId = 10, HotelComfortIconTypeId = 3 }, // comfortable

                // City Palace (Id = 11) - 2 rows
                new HotelComfortIcon { HotelId = 11, HotelComfortIconTypeId = 1 }, // popular
                new HotelComfortIcon { HotelId = 11, HotelComfortIconTypeId = 3 }, // comfortable

                // Boutique Hotel Madrid (Id = 12) - 3 rows
                new HotelComfortIcon { HotelId = 12, HotelComfortIconTypeId = 2 }, // city center
                new HotelComfortIcon { HotelId = 12, HotelComfortIconTypeId = 3 }, // comfortable
                new HotelComfortIcon { HotelId = 12, HotelComfortIconTypeId = 1 }, // popular

                // Lakeview Inn (Id = 13) - 2 rows
                new HotelComfortIcon { HotelId = 13, HotelComfortIconTypeId = 1 }, // popular
                new HotelComfortIcon { HotelId = 13, HotelComfortIconTypeId = 3 }, // comfortable

                // Ocean Breeze Hotel (Id = 14) - 2 rows
                new HotelComfortIcon { HotelId = 14, HotelComfortIconTypeId = 1 }, // popular
                new HotelComfortIcon { HotelId = 14, HotelComfortIconTypeId = 3 }, // comfortable

                // Countryside Inn (Id = 15) - 3 rows
                new HotelComfortIcon { HotelId = 15, HotelComfortIconTypeId = 1 }, // popular
                new HotelComfortIcon { HotelId = 15, HotelComfortIconTypeId = 3 }, // comfortable
                new HotelComfortIcon { HotelId = 15, HotelComfortIconTypeId = 2 }, // city center
            });
        }
    }
}
