using Mapster;
using Microsoft.AspNetCore.Mvc;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.Helper.Dtos.Classes;
using PhotonPiano.Helper.Dtos.Ultilities;
using PhotonPiano.Models.Enums;

namespace PhotonPiano.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/[controller]")]
    public class ClassesController : ControllerBase
    {
        private readonly IStudentClassService _studentClassService;
        private readonly IClassService _classService;

        public ClassesController(IStudentClassService studentClassService, IClassService classService)
        {
            _studentClassService = studentClassService;
            _classService = classService;
        }
        //[HttpGet("test")]
        //public async Task<ActionResult<List<GetClassDto>>> Test1([FromQuery] ScheduleClassesOption option) 
        //{
        //    return (await _classService.GetClassesBasedOnOption(option)).Adapt<List<GetClassDto>>();
        //}
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<GetClassWithTotalsDto>>> GetPagedClasses(
            [FromQuery] QueryClassDto queryClassDto,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            return await _classService.GetPagedClasses(pageNumber,pageSize,queryClassDto);
        }

        [HttpGet("{classId}")]
        public async Task<ActionResult<GetClassDetailDto?>> GetClassDetail(
            [FromRoute] long classId)
        {
            return await _classService.GetClassDetail(classId);
        }
        [HttpPost]
        public async Task<ActionResult<GetClassDto>> CreateClass([FromBody] CreateClassDto createClassDto)
        {
            return await _classService.CreateClass(createClassDto);
        }
        [HttpPatch]
        public async Task<ActionResult<GetClassDto>> UpdateClass([FromBody] UpdateClassDto updateClassDto)
        {
            await _classService.UpdateClass(updateClassDto);
            return NoContent();
        }
        [HttpPatch("{classId}/announce")]
        public async Task<IActionResult> AnnounceAClass([FromRoute] long classId)
        {
            await _classService.AnnounceAClass(classId);
            return Ok();
        }
        [HttpPatch("announce-all")]
        public async Task<IActionResult> AnnounceAllClass()
        {
            await _classService.AnnounceAllClass();
            return Ok();
        }
    }
}
