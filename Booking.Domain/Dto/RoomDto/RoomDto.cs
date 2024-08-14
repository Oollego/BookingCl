
namespace Booking.Domain.Dto.RoomDto
{
    public record RoomDto(
        long Id,
        string RoomName,
        decimal RoomPrice,
        string Logo,
        decimal Cancellation,
        int Guests
        );


}
