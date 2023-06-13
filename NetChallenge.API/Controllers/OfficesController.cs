using System.Collections.Generic;
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
    public class OfficesController : ControllerBase
    {
        private readonly ILogger<OfficesController> _logger;
        private readonly IOfficeRentalService _officeRentalService;

        public OfficesController(ILogger<OfficesController> logger, IOfficeRentalService officeRentalService)
        {
            _logger = logger;
            _officeRentalService = officeRentalService;
        }
        
        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<OfficeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult Get(string locationName)
        {
            if (string.IsNullOrWhiteSpace(locationName))
                return BadRequest("Location name is required");
            
            var offices = _officeRentalService.GetOffices(locationName);

            if (offices.Any()) return Ok(offices);
            
            _logger.LogInformation("[{OfficesControllerName}] ... No offices found for location {LocationName} ...",
                nameof(OfficesController), locationName);
                
            return NoContent();
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult Post(AddOfficeRequest payload)
        {
            _officeRentalService.AddOffice(payload);

            return Ok(); 
        }
        
        [HttpPost]
        [Route("suggestions")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<OfficeDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult GetSuggestions(SuggestionsRequest payload)
        {
            var offices = _officeRentalService.GetOfficeSuggestions(payload);

            if (offices.Any()) return Ok(offices);
            
            _logger.LogInformation(
                "[{OfficesControllerName}] ... No office suggestions found for requested criteria ...",
                nameof(OfficesController));
                
            return NoContent();

        }
    }
}