using Microsoft.EntityFrameworkCore;
using Data.Entities;

namespace Data.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options) { }

    //bara tillfällig konstruktor då det krånglade för mig
    public DataContext() : base(new DbContextOptionsBuilder<DataContext>()
    .UseSqlServer("Server=tcp:hagg-server.database.windows.net,1433;Initial Catalog=hagg-server;Persist Security Info=False;User ID=JenniferHagg;Password=Mirtan123!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;")
    .Options)
    { }

    public DbSet<BookingEntity> Bookings { get; set; } = null!;
    public DbSet<BookingOwnerEntity> BookingOwners { get; set; } = null!;
    public DbSet<BookingAddressEntity> BookingAddresses { get; set; } = null!;
}