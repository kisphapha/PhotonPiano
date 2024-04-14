using Microsoft.AspNetCore.Mvc;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.Helper.Dtos.Students;
using PhotonPiano.Models.Models;

namespace PhotonPiano.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetStudentWithUserDto>>> GetPagedStudentList([FromQuery] QueryStudentDto queryStudentDto,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            return await _studentService.GetPagedStudentList(pageNumber,pageSize,queryStudentDto);
        }

        [HttpGet("{studentId}")]
        public async Task<ActionResult<GetStudentProfileDto?>> GetStudentDetail([FromRoute] long studentId)
        {
            return await _studentService.GetStudentDetailById(studentId);
        }

        [HttpGet("{studentId}/posts")]
        public async Task<ActionResult<GetStudentWithPostsDto?>> GetStudentWithPostsAndComments([FromRoute] long studentId)
        {
            return await _studentService.GetStudentWithPostsAndComments(studentId);
        }


        [HttpPatch("{studentId}/change-status")]
        public async Task<IActionResult> UpdateStatusStudent([FromRoute] long studentId, [FromQuery] string status)
        {
            await _studentService.ChangeStatusOfStudent(studentId,status);
            return NoContent();
        }

        [HttpPatch("{studentId}/change-short-desc")]
        public async Task<IActionResult> UpdateShortDescription([FromRoute] long studentId, [FromBody] UpdateShortDescriptionDto description)
        {
            await _studentService.UpdateStudentShortDescription(studentId, description.content);
            return NoContent();
        }
    }
}
