using Asp.Versioning;
using Azure;
using Booking.Domain.Dto.RoomDto;
using Booking.Domain.Entity;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService = null!;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoomDto>>> GetRoom(long id)
        {
            var response = await _roomService.GetRoomByIdAsync(id);

            if (response.IsSuccess)
            { 
                return Ok(response);
            }
            return BadRequest(response);
        }


        [HttpGet("rooms/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoomDto>>> GetHotelRooms(long id)
        {
            var response = await _roomService.GetRoomsAsync(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoomDto>>> Delete(long id)
        {
            var response = await _roomService.DeleteRoomAsync(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Create new room
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// Request for create report:
        /// 
        ///     POST
        ///     {
        ///         "RoomName": "Room name",
        ///         "RoomPrice": "80.00",
        ///         "Logo": "Logo.png",
        ///         "Cancellation": "80.00",
        ///         "Guests": "3",
        ///         "HotelId": "1"
        ///     }
        ///     
        /// </remarks>
        /// <response code="200">If room created</response>
        /// <response code="400">If room wasn't created</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoomDto>>> Create([FromBody] CreateRoomDto dto)
        {
            var response = await _roomService.CreatRoomAsync(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Update room
        /// </summary>
        /// <param name="dto"></param>
        /// <remarks>
        /// </remarks>
        /// <response code="200">If room created</response>
        /// <response code="400">If room wasn't created</response>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoomDto>>> Update([FromBody] UpdateRoomDto dto)
        {
            var response = await _roomService.UpdateRoomAsync(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
