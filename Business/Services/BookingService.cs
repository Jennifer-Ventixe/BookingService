using Business.Models;
using Data.Entities;
using Data.Repositories;
using Microsoft.Extensions.Logging;

namespace Business.Services;

public class BookingService(IBookingRepository bookingRepository) : IBookingService
{
    private readonly IBookingRepository _bookingRepository = bookingRepository;

    public async Task<BookingResult> CreateBookingAsync(CreateBookingRequest request)
    {
        var bookingEntity = new BookingEntity
        {
            EventId = request.EventId,
            BookingDate = DateTime.UtcNow,
            TicketQuantity = request.TicketQuantity,
        };

        var bookingOwnerEntity = new BookingOwnerEntity
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
        };

        var bookingAddressEntity = new BookingAddressEntity
        {
            StreetName = request.StreetName,
            PostalCode = request.PostalCode,
            City = request.City,
        };

        var result = await _bookingRepository.AddAsync(bookingEntity);
        return result.Success
            ? new BookingResult
            { Success = true }
            : new BookingResult { Success = false, Error = result.Error };
    }

    public async Task<BookingResult<IEnumerable<Booking>>> GetBookingsAsync()
    {
        var result = await _bookingRepository.GetAllAsync();
        var bookings = result.Result?.Select(x => new Booking
        {
            id = x.Id,
            EventId = x.EventId,
            FirstName = x.BookingOwner?.FirstName ?? string.Empty,
            LastName = x.BookingOwner?.LastName ?? string.Empty,
            Email = x.BookingOwner?.Email ?? string.Empty,
            StreetName = x.BookingAddress?.StreetName ?? string.Empty,
            PostalCode = x.BookingAddress?.PostalCode ?? string.Empty,
            City = x.BookingAddress?.City ?? string.Empty,
            TicketQuantity = x.TicketQuantity
        });

        return new BookingResult<IEnumerable<Booking>> { Success = true, Result = bookings };
    }


    public async Task<BookingResult<Booking?>> GetBookingAsync(string bookingId)
    {
        var result = await _bookingRepository.GetAsync(x => x.Id == bookingId);
        if (result.Success && result.Result != null)
        {
            var currentBooking = new Booking
            {
                id = result.Result.Id,
                EventId = result.Result.EventId,
                FirstName = result.Result.BookingOwner?.FirstName ?? string.Empty,
                LastName = result.Result.BookingOwner?.LastName ?? string.Empty,
                Email = result.Result.BookingOwner?.Email ?? string.Empty,
                StreetName = result.Result.BookingAddress?.StreetName ?? string.Empty,
                PostalCode = result.Result.BookingAddress?.PostalCode ?? string.Empty,
                City = result.Result.BookingAddress?.City ?? string.Empty,
                TicketQuantity = result.Result.TicketQuantity

            };
            return new BookingResult<Booking?> { Success = true, Result = currentBooking };
        }

        return new BookingResult<Booking?> { Success = false, Error = result.Error ?? "Booking not found." };

    }

}