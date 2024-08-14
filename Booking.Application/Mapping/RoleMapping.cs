using AutoMapper;
using Booking.Domain.Dto.Role;
using Booking.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Mapping
{
    public class RoleMapping: Profile
    {
        public RoleMapping() 
        {
            CreateMap<Role, RoleDto>().ReverseMap();
        }
    }
}
