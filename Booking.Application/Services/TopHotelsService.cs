using Booking.Application.Resources;
using Booking.Domain.Dto.RoomDto;
using Booking.Domain.Dto.TopHotelDto;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Services
{
    public class TopHotelsService : ITopHotelsService
    {
        private readonly IBaseRepository<Hotel> _hotelRepository = null!;
        private readonly IBaseRepository<Review> _reviewRepository = null!;

        public TopHotelsService(IBaseRepository<Hotel> hotelRepository, IBaseRepository<Review> reviewRepository)
        {
            _hotelRepository = hotelRepository;
            _reviewRepository = reviewRepository;
        }

        public async Task<CollectionResult<TopHotelDto>> GetTopHotels(int qt)
        {
            try
            {
                var hotel = _hotelRepository.GetAll();
                var review = _reviewRepository.GetAll().Include(x => x.Hotel);
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
            }
                
            //    Select(p => new
            //{
            //    FacScore = p.FacilityScore,
            //    StScore = p.StaffScore,
            //    ClScore = p.CleanlinessScore,
            //    ComScore = p.ComfortScore,
            //    LocScore = p.LocationScore,
            //    ValScore = p.ValueScore
            //}).;
            //_hotelRepository.GetAll()
            //    .Include(x => x.Reviews).Select(p => new 
            //    {
            //        FasScore = p.Reviews.
            //    });
            //.Include(x => x.Rooms)
            //.ThenInclude(x => x.Books).Count()

            return new CollectionResult<TopHotelDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }
    }
}
