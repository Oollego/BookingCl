﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class RoomPhoto
    {
        public long Id { get; set; }
        public string PhotoName { get; set; } = null!;
        public long RoomId { get; set; }
        public Room Room { get; set; } = null!;
    }
}
