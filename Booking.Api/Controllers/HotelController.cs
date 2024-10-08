﻿using Asp.Versioning;
using Booking.Application.Services;
using Booking.Domain.Dto.Facility;
using Booking.Domain.Dto.Hotel;
using Booking.Domain.Dto.HotelInfoCell;
using Booking.Domain.Dto.Review;
using Booking.Domain.Dto.Room;
using Booking.Domain.Dto.SearchFilter;
using Booking.Domain.Interfaces.Services;
using Booking.Domain.Result;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Booking.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HotelController: ControllerBase
    {
        private IHotelService _hotelService;
        private IRoomService _roomService;
        private IReviewService _reviewService;
        private IFacilityService _facilityService;
        private IInfoCellService _infoCellService;

        public HotelController(IHotelService hotelService, IRoomService roomService, IReviewService reviewService, 
            IFacilityService facilityService, IInfoCellService infoCellService)
        {
            _hotelService = hotelService;
            _roomService = roomService;
            _reviewService = reviewService;
            _facilityService = facilityService;
            _infoCellService = infoCellService;
        }

        /// <summary>
        /// "https://localhost:44345/tophotels?qty=3&avgReview=6&api-version=1"
        /// </summary>
        /// <param name="qty"></param>
        /// <param name="rating"></param>
        /// <returns></returns>
        [HttpGet("info/tophotels")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<TopHotelDto>>> GetTopHotel(int qty, int rating)
        {
            var response = await _hotelService.GetTopHotelsAsync(qty, rating);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        /// <summary>
        /// Get rooms of hotel using hotelId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("info/rooms/{id}")]
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

        [HttpPatch("info/search_hotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<HotelDto>>> SearchHotels([FromBody] SearchHotelDto dto)
        {

            var response = await _hotelService.SearchHotelAsync(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPatch("info/search_filters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<SearchFilterResponseDto>>> SearchFilters([FromBody] SearchFilterDto dto)
        {
 
            var response = await _hotelService.GetSearchFiltersAsync(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("info/hotel/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<InfoHotelDto>>> GetHotelInfo(long id)
        {
            var user = HttpContext.User.Identity as ClaimsIdentity;
            string? email = null;

            if (user is not null && user.IsAuthenticated)
            {
                 email = user.Claims.First().Value;
            }

            var response = await _hotelService.GetHotelInfoAsync(id, email);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("info/facilities/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CollectionResult<FacilityInfoDto>>> GetHotelFacilitiesInfo(long id)
        {
            var response = await _facilityService.GetHotelFacilitiesAsync(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("info/cells/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CollectionResult<HotelInfoCellDto>>> GetHotelCellsInfo(long id)
        {
            var response = await _infoCellService.GetHotelInfoCellsAsync(id);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpGet("info/reviews/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CollectionResult<HotelReviewDto>>> GetHotelReviewsInfo(long id, int qty)
        {
           
            var response = await _reviewService.GetHotelReviewsAsync(id, qty);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        [HttpPost("hotel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BaseResult<InfoHotelDto>>> CreatHotel(CreateHotelDto dto)
        {
            //var user = HttpContext.User.Identity as ClaimsIdentity;
            //string? email = null;

            //if (user is not null && user.IsAuthenticated)
            //{
            //    email = user.Claims.First().Value;
            //}

            var response = await _hotelService.CreateHotelAsync(dto);

            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
