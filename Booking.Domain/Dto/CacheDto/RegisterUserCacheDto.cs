using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.CasheDto
{
    public class RegisterUserCacheDto
    {
        public string Id { get; set; } = null!;
        public string UserEmail { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public string PasswordDk { get; set; } = null!;
    }
}
