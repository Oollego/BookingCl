using Booking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class Room : IEntityId<long>
    {
        public long Id { get; set; }
        public string RoomName { get; set; } = null!;
        public decimal RoomPrice { get; set; }
        public string Logo {  get; set; } = null!;
        public decimal Cancellation { get; set; }
        public int Guests { get; set; }
        public long HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;
        public List<RoomPhoto> RoomPhotos { get; set; } = null!;
        public List<BedType> BedTypes { get; set; } = null!;
        public List<RoomFacilityType> RoomFacilityTypes { get; set; } = null!;
        public List<Book> Books { get; set; } = null!;
        public List<RoomComfortIconType> RoomComfortIcons { get; set; } = null!;
    }
}
