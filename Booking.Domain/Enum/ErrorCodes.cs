using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Enum
{
    public enum ErrorCodes
    {
        RoomsNotFound = 0,
        RoomNotFound = 1,
        RoomAlreadyExists = 2,
        InternalServerError = 10,

        UserNotFound = 11,
        UserAlreadyExists = 12,
        UserUnauthorizedAccess = 13,
        UserAlreadyExistsThisRole = 14,
        UserWasNotCreated = 15,

        PasswordNotEqualsPasswordConfirm = 21,
        PasswordIsWrong = 22,
        EmailIsNotCorrect = 23,
        RegistrationCodeNotFound = 24,
        UserIsNotMatched = 25,

        RoleAlreadyExists = 31,
        RoleNotFound = 32,
        

        HotelNotFound = 41


    }
}
