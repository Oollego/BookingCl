using AutoMapper;
using Booking.Domain.Dto.RoomDto;
using Booking.Domain.Entity;

namespace Booking.Application.Mapping
{
    public class RoomMapping: Profile
    {
        public RoomMapping() 
        { 
            CreateMap<Room, RoomDto>().ReverseMap();
        }
    }
}
