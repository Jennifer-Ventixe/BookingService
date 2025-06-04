

using Data.Contexts;
using Data.Entities;

namespace Data.Repositories;

public class BookingOwnerRepository(DataContext context) : BaseRepository<BookingOwnerEntity>(context), IBookingOwnerRepository
{
}