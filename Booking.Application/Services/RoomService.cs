using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Entity;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Booking.Application.Resources;
using Booking.Domain.Enum;
using Booking.Domain.Dto.RoomDto;
using Booking.Domain.Interfaces.Validations;
using System.Runtime.CompilerServices;
using AutoMapper;
using Booking.Domain.Dto.User;

namespace Booking.Application.Services
{
    public class RoomService : IRoomService
    {
        private readonly IBaseRepository<Room> _roomRepository = null!;
        private readonly IBaseRepository<Hotel> _hotelRepository = null!;
        private readonly IRoomValidator _roomValidator;
        private readonly IMapper _mapper;
        private readonly ILogger _logger = null!;


        public RoomService(IBaseRepository<Room> roomRepository, ILogger logger, 
            IBaseRepository<Hotel> hotelRepository, IRoomValidator roomValidator, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _logger = logger;
            _hotelRepository = hotelRepository;
            _roomValidator = roomValidator;
            _mapper = mapper;
        }

        /// < inheritdoc />
        public async Task<BaseResult<RoomDto>> CreatRoomAsync(CreateRoomDto dto)
        {
            try
            {
                var hotel = await _hotelRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.HotelId);
                var room = await _roomRepository.GetAll().FirstOrDefaultAsync(x => x.RoomName == dto.RoomName);
                var result = _roomValidator.CreateValidator(room, hotel);

                if (!result.IsSuccess)
                {
                    return new BaseResult<RoomDto>()
                    {
                        ErrorMessage = result.ErrorMessage,
                        ErrorCode = result.ErrorCode,
                    };
                }

                room = new Room()
                {
                    RoomName = dto.RoomName,
                    RoomPrice = dto.RoomPrice,
                    Logo = dto.Logo,
                    Cancellation = dto.Cancellation,
                    Guests = dto.Guests,
                    HotelId = dto.HotelId,
                };

                await _roomRepository.CreateAsync(room);

                return new BaseResult<RoomDto>()
                {
                    Data = _mapper.Map<RoomDto>(room),
                };

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<RoomDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        /// < inheritdoc />
        public async Task<BaseResult<RoomDto>> DeleteRoomAsync(long roomId)
        {
            try
            {
                var room = await _roomRepository.GetAll().FirstOrDefaultAsync(x => x.Id == roomId);
                // var result = _roomValidator.ValidateOnNull(room);
                //if (!result.IsSuccess)
                //{
                //    return new BaseResult<RoomDto>()
                //    {
                //        ErrorMessage = result.ErrorMessage,
                //        ErrorCode = result.ErrorCode,
                //    };
                //}
                if (room == null)
                {
                    return new BaseResult<RoomDto>()
                    {
                        ErrorMessage = ErrorMessage.RoomNotFound,
                        ErrorCode = (int)ErrorCodes.RoomNotFound,
                    };
                }

                _roomRepository.Remove(room);
                await _roomRepository.SaveChangesAsync();
                return new BaseResult<RoomDto>()
                {
                    Data = _mapper.Map<RoomDto>(room)
                };

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<RoomDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }

        public Task<BaseResult<RoomDto>> GetRoomByIdAsync(long roomId)
        {
            RoomDto? room;
            try
            {
                room = _roomRepository.GetAll()
                    .Where(x => x.Id == roomId)
                    .Select(x => new RoomDto(x.Id, x.RoomName, x.RoomPrice, x.Logo, x.Cancellation, x.Guests))
                    .AsEnumerable()
                    .FirstOrDefault(x => x.Id == roomId);

                //room = await _roomRepository.GetAll()
                //    .Select(x => new RoomDto(x.Id, x.RoomName, x.RoomPrice, x.Logo, x.Cancellation, x.Guests))
                //    .FirstOrDefaultAsync(x => x.Id == roomId);
                //rooms = await _roomRepository.GetAll()
                //   .Where(x => x.Hotel.Id == hotelId)
                //   .Select(x => new RoomDto(x.Id, x.RoomName, x.RoomPrice, x.Logo, x.Cancellation, x.Guests))
                //   .ToArrayAsync();
                //room = _roomRepository.GetAll().AsEnumerable()
                //  .Select(x => new RoomDto(x.Id, x.RoomName, x.RoomPrice, x.Logo, x.Cancellation, x.Guests))
                //  .FirstOrDefault(x => x.Id == roomId);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return Task.FromResult(new BaseResult<RoomDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                });
            }

            if (room == null) 
            {
                _logger.Warning( $"Room id:{roomId} not found");
                return Task.FromResult(new BaseResult<RoomDto>()
                {
                    ErrorMessage = ErrorMessage.RoomNotFound,
                    ErrorCode = (int)ErrorCodes.RoomNotFound
                });
            }

            return Task.FromResult(new BaseResult<RoomDto>()
            {
                Data = room
            });
        }

        /// < inheritdoc />
        public async Task<CollectionResult<RoomDto>> GetRoomsAsync(long hotelId)
        {
            RoomDto[] rooms;
            try
            {
                rooms = await _roomRepository.GetAll()
                    .Where(x => x.Hotel.Id == hotelId)
                    .Select(x => new RoomDto(x.Id, x.RoomName, x.RoomPrice, x.Logo, x.Cancellation, x.Guests))
                    .ToArrayAsync();
            }
            catch (Exception ex) 
            {
                _logger.Error(ex, ex.Message);
                return new CollectionResult<RoomDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }

            if (!rooms.Any())
            {
                _logger.Warning(ErrorMessage.RoomsNotFound, rooms.Length);
                return new CollectionResult<RoomDto>()
                {
                    ErrorMessage = ErrorMessage.RoomsNotFound,
                    ErrorCode = (int)ErrorCodes.RoomsNotFound
                };
            }

            return new CollectionResult<RoomDto>()
            {
                Data = rooms,
                Count = rooms.Length
            };
        }

        public async Task<BaseResult<RoomDto>> UpdateRoomAsync(UpdateRoomDto dto)
        {
            try
            {
                var room = await _roomRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);

                if (room == null)
                {
                    return new BaseResult<RoomDto>()
                    {
                        ErrorMessage = ErrorMessage.RoomNotFound,
                        ErrorCode = (int)ErrorCodes.RoomNotFound,
                    };
                }
                //TO DO ubrat ValidateOnNull
               // var result = _roomValidator.ValidateOnNull(room);

                //if (!result.IsSuccess)
                //{
                    //return new BaseResult<RoomDto>()
                    //{
                    //    ErrorMessage = result.ErrorMessage,
                    //    ErrorCode = result.ErrorCode,
                    //};
                //}
             
                room.RoomName = dto.RoomName;
                room.RoomPrice = dto.RoomPrice;
                room.Logo = dto.Logo;
                room.Cancellation = dto.Cancellation;
                room.Guests = dto.Guests;

                var updatedRoom = _roomRepository.Update(room);
                await _roomRepository.SaveChangesAsync();

                return new BaseResult<RoomDto>()
                {
                    Data = _mapper.Map<RoomDto>(updatedRoom)
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                return new BaseResult<RoomDto>()
                {
                    ErrorMessage = ErrorMessage.InternalServerError,
                    ErrorCode = (int)ErrorCodes.InternalServerError
                };
            }
        }
    }

}
