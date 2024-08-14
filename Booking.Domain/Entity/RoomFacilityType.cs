using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class RoomFacilityType
    {
        public long Id { get; set; }
        public string RoomFacilityName { get; set; } = null!;
        public string RoomFacilityIcon { get; set; } = null!;
        public List<Room> Rooms { get; set; } = null!;
    }
}
