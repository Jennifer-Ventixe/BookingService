using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController(IBookingService bookingService) : ControllerBase
{
    private readonly IBookingService _bookingService = bookingService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var events = await _bookingService.GetBookingsAsync();
        return Ok(events);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var currentEvent = await _bookingService.GetBookingAsync(id);
        return currentEvent != null ? Ok(currentEvent) : NotFound();

    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookingRequest request)
    {
       if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await _bookingService.CreateBookingAsync(request);
        return result.Success ? Ok() : StatusCode(StatusCodes.Status500InternalServerError, "Unable to create booking.");
    }

}


