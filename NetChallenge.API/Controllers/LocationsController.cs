using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
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
    public class LocationsController : ControllerBase
    {
        private readonly ILogger<LocationsController> _logger;
        private readonly IOfficeRentalService _officeRentalService;

        public LocationsController(ILogger<LocationsController> logger, IOfficeRentalService officeRentalService)
        {
            _logger = logger;
            _officeRentalService = officeRentalService;
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<LocationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Get()
        {
            var locations = _officeRentalService.GetLocations();

            if (locations.Any()) return Ok(locations);
            
            _logger.LogInformation("[{LocationsControllerName}] ... No locations found ...",
                nameof(LocationsController));
                
            return NoContent();
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult Post(AddLocationRequest payload)
        {
            _officeRentalService.AddLocation(payload);

            return Ok(); 
        }
    }
}