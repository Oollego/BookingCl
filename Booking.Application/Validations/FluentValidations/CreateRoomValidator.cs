﻿using Booking.Domain.Dto.RoomDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Validations.FluentValidations
{
    public class CreateRoomValidator: AbstractValidator<CreateRoomDto>
    {
        public CreateRoomValidator() 
        {
            RuleFor(x => x.RoomName).NotEmpty().MaximumLength(254);
            RuleFor(x => x.Guests).NotEmpty();
        }

    }
}
