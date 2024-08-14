
namespace Booking.Domain.Dto.RoomDto
{
    public record CreateRoomDto(
       string RoomName,
       decimal RoomPrice,
       string Logo,
       decimal Cancellation,
       int Guests,
       long HotelId
     );
}
