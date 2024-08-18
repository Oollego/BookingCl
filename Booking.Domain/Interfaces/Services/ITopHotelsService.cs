using Booking.Domain.Dto.TopHotelDto;
using Booking.Domain.Dto.User;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Services
{
    public interface ITopHotelsService
    {
        Task<CollectionResult<TopHotelDto>> GetTopHotels(int qt);
    }
}
