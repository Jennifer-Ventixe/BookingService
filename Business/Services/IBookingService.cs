using Business.Models;

namespace Business.Services
{
    public interface IBookingService
    {
        Task<BookingResult> CreateBookingAsync(CreateBookingRequest request);
        Task<BookingResult<Booking?>> GetBookingAsync(string bookingId);
        Task<BookingResult<IEnumerable<Booking>>> GetBookingsAsync();
    }
}