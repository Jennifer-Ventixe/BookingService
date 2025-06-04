using Microsoft.EntityFrameworkCore;
using Data.Entities;
using System.Collections.Generic;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<BookingEntity> Bookings { get; set; } = null!;
    public DbSet<BookingOwnerEntity> BookingOwners { get; set; } = null!;
    public DbSet<BookingAddressEntity> BookingAddresses { get; set; } = null!;
}
