using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.Hotel
{
    public class SearchHotelDto
    {
        public string Place { get; set; } = null!;
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Rooms { get; set; }
        public List<int> Stars { get; set; } = null!;
        public List<int> Rating { get; set; } = null!;
        public List<long> Popular { get; set; } = null!;
        public List<long> Facilities { get; set; } = null!;
        public List<long> TypeOfHotel { get; set; } = null!;
        public List<long> ChainHotels { get; set; } = null!;

    }
}
