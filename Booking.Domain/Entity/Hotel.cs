using Booking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class Hotel: IEntityId<long>
    {
        public long Id { get; set; }
        public string HotelName { get; set; } = null!;
        public string HotelAddress { get; set; } = null!;
        public string HotelPhone { get; set; } = null!;
        public string HotelPhoto { get; set; } = null!;
        public string CityGuide { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int FixedDays { get; set; }
        public bool IsPet { get; set; }
        public List<Room> Rooms { get; set; } = null!;
        public List<HotelInfoCell> HotelInfoCells { get; set; } = null!;
        public List<NearPlace> NearPlaces { get; set; } = null!;
        public List<Review> Reviews { get; set; } = null!;
        public List<Facility> Facilities { get; set; } = null!;
    }
}
