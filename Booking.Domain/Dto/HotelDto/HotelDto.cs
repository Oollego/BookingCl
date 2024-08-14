using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.HotelDto
{
    public record HotelDto(
        long Id,
        string HotelName,
        string HotelAddress,
        string HotelPhone,
        string HotelPhoto,
        string CityGuide,
        string Description,
        int FixedDays,
        bool IsPet
        );
}
