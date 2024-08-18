using Booking.Domain.Dto.RoomDto;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    [ApiController]
    
    public class TopHotelController: ControllerBase
    {
        private ITopHotelsService _topHotelsService;

        public TopHotelController(ITopHotelsService topHotelsService)
        {
            _topHotelsService = topHotelsService;
        }

        [HttpGet("{qt}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<RoomDto>>> GetHotel(int qt)
        {
            var response = await _topHotelsService.GetTopHotels(qt);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
