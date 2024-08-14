using AutoMapper;
using Booking.Application.Resources;
using Booking.Domain.Dto;
using Booking.Domain.Dto.RoomDto;
using Booking.Domain.Dto.User;
using Booking.Domain.Entity;
using Booking.Domain.Enum;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Interfaces.UnitsOfWork;

namespace Booking.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseRepository<User> _userRepository = null!;
        private readonly ILogger _logger = null!;
        private readonly IMapper _mapper = null!;
        private readonly IBaseRepository<UserToken> _userTokenRepository = null!;
        private readonly ITokenService _tokenService;
        private readonly IBaseRepository<Role> _roleRepository = null!;
          private readonly IUnitOfWork _unitOfWork = null!;

        public AuthService(IBaseRepository<User> userRepository, ILogger logger, IMapper mapper,
            IBaseRepository<UserToken> userTokenRepository, ITokenService tokenService, 
            IBaseRepository<Role> roleRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
            _userTokenRepository = userTokenRepository;
            _tokenService = tokenService;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResult<UserDto>> Register(RegisterUserDto dto)
        {
           // throw new UnauthorizedAccessException("UnauthorizedAccessException");
            if(dto.Password != dto.PasswordConfirm)
            {
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessage.PasswordNotEqualsPasswordConfirm,
                    ErrorCode = (int)ErrorCodes.PasswordNotEqualsPasswordConfirm,

                };
            }
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.UserEmail == dto.Email);
                if(user != null)
                {
                    return new BaseResult<UserDto>()
                    {
                        ErrorMessage = ErrorMessage.UserAlreadyExists,
                        ErrorCode = (int)ErrorCodes.UserAlreadyExists
                    };
                }
                var hashUserPassword = HashPassword(dto.Password);

                using (var transaction = await _unitOfWork.BeginTransactionAsync())
                {
                    try
                    {
                        user = new User()
                        {
                            UserEmail = dto.Email,
                            PasswordDk = hashUserPassword,
                            PasswordSalt = hashUserPassword,
                        };
                        await _unitOfWork.Users.CreateAsync(user);
                        await _unitOfWork.SaveChangesAsync();

                        var role = await _roleRepository.GetAll().FirstOrDefaultAsync(x => x.RoleName == nameof(Roles.User));
                        if (role == null)
                        {
                            return new BaseResult<UserDto>()
                            {
                                ErrorMessage = ErrorMessage.RoleNotFound,
                                ErrorCode = (int)ErrorCodes.RoleNotFound
                            };
                        }

                        UserRole userRole = new UserRole()
                        {
                            UserId = user.Id,
                            RoleId = role.Id
                        };

                        await _unitOfWork.UserRoles.CreateAsync(userRole);

                        await _unitOfWork.SaveChangesAsync();

                        await transaction.CommitAsync();

                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();

                        return new BaseResult<UserDto>()
                        {
                            ErrorMessage = ErrorMessage.UserWasn_tCreated,
                            ErrorCode = (int)ErrorCodes.UserWasNotCreated
                        };
                    }
                }
                              
                return new BaseResult<UserDto>()
                {
                    Data = _mapper.Map<UserDto>(user)
                };
            }
            catch (Exception ex) 
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<UserDto>()
                {
                    ErrorMessage = ErrorMessage.InternalIServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        public async Task<BaseResult<TokenDto>> Login(LoginUserDto dto)
        {
            try
            {
                var user = await _userRepository.GetAll()
                    .Include(x => x.Roles)
                    .FirstOrDefaultAsync(x => x.UserEmail == dto.Email);
                if (user == null)
                {
                    return new BaseResult<TokenDto>()
                    {
                        ErrorMessage = ErrorMessage.UserNotFound,
                        ErrorCode = (int)ErrorCodes.UserNotFound
                    };
                }
                
                if (!IsVerifyPassword(user.PasswordDk, dto.Password)) 
                {
                    return new BaseResult<TokenDto>()
                    {
                        ErrorMessage = ErrorMessage.PasswordIsWrong,
                        ErrorCode = (int)ErrorCodes.PasswordIsWrong
                    };
                }

                var userToken = await _userTokenRepository.GetAll().FirstOrDefaultAsync(x => x.UserId == user.Id);

                var userRoles = user.Roles;
                var claims = userRoles.Select(x => new Claim(ClaimTypes.Role, x.RoleName)).ToList();
                claims.Add(new Claim(ClaimTypes.Email, dto.Email));
               
                var accessToken = _tokenService.GenerateAccessToken(claims);
                var refreshToken = _tokenService.GenerateRefreshToken();

                if (userToken == null)
                {
                    userToken = new UserToken()
                    {
                        UserId = user.Id,
                        RefreshToken = refreshToken,
                        RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(10)
                    };
                    await _userTokenRepository.CreateAsync(userToken);
                }
                else
                {
                    userToken.RefreshToken = refreshToken;
                    userToken.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(10);

                    _userTokenRepository.Update(userToken);
                    await _userTokenRepository.SaveChangesAsync();
                }

                return new BaseResult<TokenDto>()
                {
                    Data = new TokenDto()
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<TokenDto>()
                {
                    ErrorMessage = ErrorMessage.InternalIServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        private string HashPassword(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private bool IsVerifyPassword(string UserPasswordHash, string userPassword)
        {
            var hash = HashPassword(userPassword);
            return UserPasswordHash == hash;
        }
    }
}
