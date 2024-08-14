
namespace Booking.Domain.Dto.RoomDto
{
    public record UpdateRoomDto(
        long Id,
        string RoomName,
        decimal RoomPrice,
        string Logo,
        decimal Cancellation,
        int Guests
    );
}
