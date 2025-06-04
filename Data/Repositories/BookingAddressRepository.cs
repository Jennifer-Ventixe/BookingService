

using Data.Contexts;
using Data.Entities;

namespace Data.Repositories;

public class BookingAddressRepository(DataContext context) : BaseRepository<BookingAddressEntity>(context), IBookingAddressRepository
{
}
