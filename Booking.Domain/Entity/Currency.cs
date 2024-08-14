using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class Currency
    {
        public string CurrencyCode { get; set; } = null!;
        public string CurrencyName { get; set; } = null!;
        public string CurrencyLetter {  get; set; } = null!;
        public List<UserProfile> UserProfiles { get; set; } = null!;
    }
}
