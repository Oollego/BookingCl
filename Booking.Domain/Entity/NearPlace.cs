using Booking.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class NearPlace 
    {
        public long Id { get; set; }
        public string Distance { get; set; } = null!;
        public bool DistanceMetric { get; set; } = false;
        public long HotelId { get; set; }
        public Hotel Hotel { get; set; } = null!;
        public long NearPlaceNameId { get; set; }
        public NearPlaceName NearPlaceName { get; set; } = null!;
    }
}
