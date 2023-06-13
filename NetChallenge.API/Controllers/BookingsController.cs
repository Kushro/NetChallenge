using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetChallenge.Application.Dto.Input;
using NetChallenge.Application.Dto.Output;
using NetChallenge.Application.Services;

namespace NetChallenge.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly ILogger<BookingsController> _logger;
        private readonly IOfficeRentalService _officeRentalService;

        public BookingsController(ILogger<BookingsController> logger, IOfficeRentalService officeRentalService)
        {
            _logger = logger;
            _officeRentalService = officeRentalService;
        }
        
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(BookingDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Get(string locationName, string officeName)
        {
            if(string.IsNullOrWhiteSpace(locationName) || string.IsNullOrWhiteSpace(officeName))
                return BadRequest("Location name and office name are required");
            
            var bookings = _officeRentalService.GetBookings(locationName, officeName);

            if (bookings.Any()) return Ok(bookings);
            
            _logger.LogInformation(
                "[{BookingsControllerName}] ... No bookings found for location {LocationName} and office {OfficeName} ...",
                nameof(BookingsController), locationName, officeName);
                
            return NoContent();


        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult Post(BookOfficeRequest payload)
        {
            _officeRentalService.BookOffice(payload);

            return Ok(); 
        }
    }
}