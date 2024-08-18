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

            builder.HasData(new List<RoomPhoto>
        {
            // Photos for Single Room (Id = 1)
            new RoomPhoto { Id = 1, PhotoName = "578237709.jpg", RoomId = 1 },
            new RoomPhoto { Id = 2, PhotoName = "215800733.jpg", RoomId = 1 },
            new RoomPhoto { Id = 3, PhotoName = "215800969.jpg", RoomId = 1 },

            // Photos for Double Room (Id = 2)
            new RoomPhoto { Id = 4, PhotoName = "215800314.jpg", RoomId = 2 },
            new RoomPhoto { Id = 5, PhotoName = "110147907.jpg", RoomId = 2 },
            new RoomPhoto { Id = 6, PhotoName = "215802543.jpg", RoomId = 2 },

            // Photos for Suite (Id = 3)
            new RoomPhoto { Id = 7, PhotoName = "401492281.jpg", RoomId = 3 },
            new RoomPhoto { Id = 8, PhotoName = "167218141.jpg", RoomId = 3 },
            new RoomPhoto { Id = 9, PhotoName = "215802499.jpg", RoomId = 3 },

            // Photos for Deluxe Room (Id = 4)
            new RoomPhoto { Id = 10, PhotoName = "215800752.jpg", RoomId = 4 },
            new RoomPhoto { Id = 11, PhotoName = "372185637.jpg", RoomId = 4 },
            new RoomPhoto { Id = 12, PhotoName = "371580726.jpg", RoomId = 4 },

            // Photos for Family Room (Id = 5)
            new RoomPhoto { Id = 13, PhotoName = "110147742.jpg", RoomId = 5 },
            new RoomPhoto { Id = 14, PhotoName = "386343575.jpg", RoomId = 5 },
            new RoomPhoto { Id = 15, PhotoName = "246926985.jpg", RoomId = 5 },

            // Photos for Standard Room (Id = 6)
            new RoomPhoto { Id = 16, PhotoName = "245006813.jpg", RoomId = 6 },
            new RoomPhoto { Id = 17, PhotoName = "245006010.jpg", RoomId = 6 },
            new RoomPhoto { Id = 18, PhotoName = "245006824.jpg", RoomId = 6 },

            // Photos for Executive Suite (Id = 7)
            new RoomPhoto { Id = 19, PhotoName = "245006830.jpg", RoomId = 7 },
            new RoomPhoto { Id = 20, PhotoName = "245021477.jpg", RoomId = 7 },
            new RoomPhoto { Id = 21, PhotoName = "245006785.jpg", RoomId = 7 },

            // Photos for Mountain View Room (Id = 8)
            new RoomPhoto { Id = 22, PhotoName = "245021511.jpg", RoomId = 8 },
            new RoomPhoto { Id = 23, PhotoName = "333511081.jpg", RoomId = 8 },
            new RoomPhoto { Id = 24, PhotoName = "304574142.jpg", RoomId = 8 },

            // Photos for Ski Suite (Id = 9)
            new RoomPhoto { Id = 25, PhotoName = "455699363.jpg", RoomId = 9 },
            new RoomPhoto { Id = 26, PhotoName = "70567712.jpg", RoomId = 9 },
            new RoomPhoto { Id = 27, PhotoName = "467565298.jpg", RoomId = 9 },

            // Photos for Oceanfront Room (Id = 10)
            new RoomPhoto { Id = 28, PhotoName = "467565311.jpg", RoomId = 10 },
            new RoomPhoto { Id = 29, PhotoName = "177737946.jpg", RoomId = 10 },
            new RoomPhoto { Id = 30, PhotoName = "588813242.jpg", RoomId = 10 },

            // Photos for Luxury Suite (Id = 11)
            new RoomPhoto { Id = 31, PhotoName = "588813265.jpg", RoomId = 11 },
            new RoomPhoto { Id = 32, PhotoName = "588813256.jpg", RoomId = 11 },
            new RoomPhoto { Id = 33, PhotoName = "588813289.jpg", RoomId = 11 },

            // Photos for Grand Room (Id = 20)
            new RoomPhoto { Id = 34, PhotoName = "268368565.jpg", RoomId = 20 },
            new RoomPhoto { Id = 35, PhotoName = "268368556.jpg", RoomId = 20 },
            new RoomPhoto { Id = 36, PhotoName = "212051436.jpg", RoomId = 20 },

            // Photos for Historic Suite (Id = 21)
            new RoomPhoto { Id = 37, PhotoName = "212050872.jpg", RoomId = 21 },
            new RoomPhoto { Id = 38, PhotoName = "212051275.jpg", RoomId = 21 },
            new RoomPhoto { Id = 39, PhotoName = "99356031.jpg", RoomId = 21 },

            // Photos for City View Room (Id = 22)
            new RoomPhoto { Id = 40, PhotoName = "290857715.jpg", RoomId = 22 },
            new RoomPhoto { Id = 41, PhotoName = "164172199.jpg", RoomId = 22 },
            new RoomPhoto { Id = 42, PhotoName = "61405250.jpg", RoomId = 22 },

            // Photos for Palace Suite (Id = 23)
            new RoomPhoto { Id = 43, PhotoName = "10809767.jpg", RoomId = 23 },
            new RoomPhoto { Id = 44, PhotoName = "578710641.jpg", RoomId = 23 },
            new RoomPhoto { Id = 45, PhotoName = "339032030.jpg", RoomId = 23 },

            // Photos for Boutique Room (Id = 24)
            new RoomPhoto { Id = 46, PhotoName = "56881927.jpg", RoomId = 24 },
            new RoomPhoto { Id = 47, PhotoName = "96302217.jpg", RoomId = 24 },
            new RoomPhoto { Id = 48, PhotoName = "339032034.jpg", RoomId = 24 },

            // Photos for Gran Via Suite (Id = 25)
            new RoomPhoto { Id = 49, PhotoName = "339032086.jpg", RoomId = 25 },
            new RoomPhoto { Id = 50, PhotoName = "431757119.jpg", RoomId = 25 },
            new RoomPhoto { Id = 51, PhotoName = "432014924.jpg", RoomId = 25 },

            // Photos for Lakeview Room (Id = 26)
            new RoomPhoto { Id = 52, PhotoName = "432014924.jpg", RoomId = 26 },
            new RoomPhoto { Id = 53, PhotoName = "81091432.jpg", RoomId = 26 },
            new RoomPhoto { Id = 54, PhotoName = "81091427.jpg", RoomId = 26 },

            // Photos for Lakefront Suite (Id = 27)
            new RoomPhoto { Id = 55, PhotoName = "354322700.jpg", RoomId = 27 },
            new RoomPhoto { Id = 56, PhotoName = "354666698.jpg", RoomId = 27 },
            new RoomPhoto { Id = 57, PhotoName = "354322689.jpg", RoomId = 27 },

            // Photos for Ocean Breeze Room (Id = 28)
            new RoomPhoto { Id = 58, PhotoName = "241134673.jpg", RoomId = 28 },
            new RoomPhoto { Id = 59, PhotoName = "354322694.jpg", RoomId = 28 },
            new RoomPhoto { Id = 60, PhotoName = "241134815.jpg", RoomId = 28 },

            // Photos for Business Suite (Id = 29)
            new RoomPhoto { Id = 61, PhotoName = "354666694.jpg", RoomId = 29 },
            new RoomPhoto { Id = 62, PhotoName = "354666691.jpg", RoomId = 29 },
            new RoomPhoto { Id = 63, PhotoName = "354668277.jpg", RoomId = 29 },

            // Photos for Countryside Room (Id = 30)
            new RoomPhoto { Id = 64, PhotoName = "88036291.jpg", RoomId = 30 },
            new RoomPhoto { Id = 65, PhotoName = "484533046.jpg", RoomId = 30 },
            new RoomPhoto { Id = 66, PhotoName = "484519440.jpg", RoomId = 30 },

            // Photos for Rustic Suite (Id = 31)
            new RoomPhoto { Id = 67, PhotoName = "126696551.jpg", RoomId = 31 },
            new RoomPhoto { Id = 68, PhotoName = "94457992.jpg", RoomId = 31 },
            new RoomPhoto { Id = 69, PhotoName = "96661618.jpg", RoomId = 31 }
        });
        }
    }
}
