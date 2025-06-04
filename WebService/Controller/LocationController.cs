using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Data.Interface;
using MongoDB.Bson;
using Service.Interface;

namespace WebService.Controller
{
    [ApiController]
    [Route("api/v1/location")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService locationService;
        public LocationController(ILocationService locationService)
        {
            this.locationService = locationService;
        }
        [HttpGet("countries")]
        public async Task<IActionResult> GetAllCountries([FromQuery] string countryId)
        {
            var countries = await locationService.GetAllCountries(ObjectId.TryParse(countryId, out var parsedCountryId) ? parsedCountryId : ObjectId.Empty);
            return Ok(countries);
        }
        [HttpGet("states")]
        public async Task<IActionResult> GetAllStates([FromQuery] string countryId)
        {
            if (!ObjectId.TryParse(countryId, out var parsedCountryId))
            {
                return BadRequest("Invalid country ID format.");
            }
            var states = await locationService.GetAllStates(parsedCountryId);
            return Ok(states);
        }
        [HttpGet("cities")]
        public async Task<IActionResult> GetAllCities([FromQuery] string stateId)
        {
            if (!ObjectId.TryParse(stateId, out var parsedStateId))
            {
                return BadRequest("Invalid state ID format.");
            }
            var cities = await locationService.GetAllCities(parsedStateId);
            return Ok(cities);
        }
    }
}