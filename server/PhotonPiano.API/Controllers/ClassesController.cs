using Microsoft.AspNetCore.Mvc;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.Helper.Dtos.Classes;
using PhotonPiano.Helper.Dtos.Paginations;

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
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<GetClassWithTotalLessonDto>>> GetPagedClasses(
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
