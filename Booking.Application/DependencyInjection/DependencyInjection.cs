using AutoMapper;
using Booking.Application.Mapping;
using Booking.Application.Services;
using Booking.Application.Validations;
using Booking.Application.Validations.FluentValidations;
using Booking.Domain.Dto.RoomDto;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Interfaces.Validations;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(RoomMapping));

            var emailOptions = configuration.GetSection(nameof(EmailService));

            string smtpServer = emailOptions["SmtpServer"] ?? "";
            int    smtpPort   = Convert.ToInt32(emailOptions["SmtpPort"]);
            bool   useSsl     = Convert.ToBoolean(emailOptions["UseSsl"]);
            string login      = emailOptions["Login"] ?? "";
            string password   = emailOptions["Password"] ?? "";

            services.AddScoped<IEmailService, EmailService>( x => 
                    new EmailService(smtpServer, smtpPort, useSsl, login, password)
                );

            InitServices(services);
        }

        public static void InitServices(this IServiceCollection services) 
        {
            services.AddScoped<IHashService, HashService>();
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
