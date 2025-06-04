
using Microsoft.EntityFrameworkCore;
using Data.Contexts;
using Data.Repositories;
using Business.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));
builder.Services.AddScoped<IBookingAddressRepository, BookingAddressRepository>();
builder.Services.AddScoped<IBookingOwnerRepository, BookingOwnerRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IBookingService, BookingService>();

var app = builder.Build();
app.MapOpenApi();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("swagger/v1/swagger.json", "EventService API");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

