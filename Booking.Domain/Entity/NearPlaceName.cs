using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class NearPlaceName
    {
        public long Id {  get; set; }
        public string Name { get; set; } = null!;
        public string? Icon { get; set; } = null!;
        public List<NearPlace> NearPlaces { get; set; } = null!;
    }
}
