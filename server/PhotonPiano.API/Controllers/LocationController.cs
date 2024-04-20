using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.BusinessLogic.Services;
using PhotonPiano.Helper.Dtos.Locations;
using PhotonPiano.Helper.Dtos.Users;
using PhotonPiano.Models.Models;

namespace PhotonPiano.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetLocationDto>>> GetLocations()
        {
            return await _locationService.GetLocations();
        }

        [HttpGet("{locationId}")]
        public async Task<ActionResult<GetLocationDto?>> GetLocationById([FromRoute] long locationId)
        {
            return await _locationService.GetLocationById(locationId,true);
        }

    }
}
