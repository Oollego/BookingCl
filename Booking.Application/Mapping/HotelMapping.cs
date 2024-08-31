using AutoMapper;
using Booking.Domain.Dto.Hotel;
using Booking.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Mapping
{
    public class HotelMapping: Profile
    {
        public HotelMapping() 
        {
            CreateMap<CreateHotelResponseDto, Hotel>().ReverseMap();
        }
    }
}
