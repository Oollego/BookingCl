using Booking.Application.Mapping;
using Booking.Application.Services;
using Booking.Application.Validations;
using Booking.Application.Validations.FluentValidations;
using Booking.Domain.Dto.RoomDto;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Interfaces.Validations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(RoomMapping));

            InitServices(services);
        }

        public static void InitServices(this IServiceCollection services) 
        { 
            services.AddScoped<IRoomValidator, RoomValidator>();
            services.AddScoped<IValidator<CreateRoomDto>, CreateRoomValidator>();
            services.AddScoped<IValidator<UpdateRoomDto>, UpdateRoomValidator>();

            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRoleService, RoleService>();
        }
    }

}
