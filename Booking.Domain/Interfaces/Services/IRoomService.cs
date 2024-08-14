using Booking.Domain.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Entity;
using Booking.Domain.Dto.RoomDto;

namespace Booking.Domain.Interfaces.Services
{
    public interface IRoomService
    {
        /// <summary>
        /// Gets all hotel rooms
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        Task<CollectionResult<RoomDto>> GetRoomsAsync(long hotelId);

        /// <summary>
        /// Get room by Id
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        Task<BaseResult<RoomDto>> GetRoomByIdAsync(long roomId);

        /// <summary>
        /// Create new Room
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<RoomDto>> CreatRoomAsync(CreateRoomDto dto);

        /// <summary>
        /// Delete room by Id
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        Task<BaseResult<RoomDto>> DeleteRoomAsync(long roomId);
        /// <summary>
        /// Update room
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BaseResult<RoomDto>> UpdateRoomAsync(UpdateRoomDto dto);
    }
}
