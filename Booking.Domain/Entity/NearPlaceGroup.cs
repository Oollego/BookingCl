using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class NearPlaceGroup
    {
        public long id {  get; set; }
        public string GroupName { get; set; } = null!;
        public string GroupIcon { get; set; } = null!;
        public List<NearPlace> NearPlaces { get; set; } = null!;
    }
}
