using Booking.Domain.Dto.Hotel;
using Booking.Domain.Dto.Review;
using Booking.Domain.Dto.SearchFilter;
using Booking.Domain.Dto.User;
using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.Services
{
    public interface IHotelService
    {
        Task<CollectionResult<TopHotelDto>> GetTopHotels(int qty, int avgReview);
        Task<BaseResult<InfoHotelDto>> GetHotelInfoAsync(long hotelId, string? email);
        Task<BaseResult<SearchHotelResponseDto>> SearchHotelAsync(SearchHotelDto dto);
        Task<BaseResult<CreateHotelResponseDto>> CreateHotelAsync(CreateHotelDto dto);
        Task<BaseResult<SearchFilterResponseDto>> GetSearchFilters(SearchFilterDto dto);
    }
}
