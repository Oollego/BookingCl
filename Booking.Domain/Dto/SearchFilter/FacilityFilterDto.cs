﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Dto.SearchFilter
{
    public class FacilityFilterDto
    {
        public string FacilityName { get; set; } = null!;
        public int Matches { get; set; }
    }
}
