using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.TopHotelDto
{
    public record TopHotelDto
    (
        string hotelName,
        string city,
        string country,
        int distanceCityCenter,
        int stars,
        string price,
        string currencySymbol,
        string fileName
     );
}
