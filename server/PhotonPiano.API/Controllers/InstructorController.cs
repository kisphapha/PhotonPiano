﻿using Microsoft.AspNetCore.Mvc;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.Helper.Dtos.Instructors;
using PhotonPiano.Helper.Dtos.Paginations;
using PhotonPiano.Helper.Dtos.Students;
using PhotonPiano.Models.Models;

namespace PhotonPiano.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/[controller]")]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _instructorService;

        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<GetInstructorWithUserDto>>> GetInstructorList([FromQuery] QueryInstructorDto queryInstructorDto,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            return await _instructorService.GetPagedInstructors(pageNumber,pageSize, queryInstructorDto);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<GetInstructorDto>>> GetAllSimpleInstructors()
        {
            return await _instructorService.GetAllSimpleInstructors();
        }
    }
}
