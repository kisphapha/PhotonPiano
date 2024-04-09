using Microsoft.AspNetCore.Mvc;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.Helper.Dtos.Classes;

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
        [HttpGet("{classId}")]
        public async Task<ActionResult<GetClassWithInstructorAndLessonsDto?>> GetClassDetail(
            [FromRoute] long classId)
        {
            return await _classService.GetClassDetail(classId);
        }
        //[HttpGet("{classId}/student-lessons")]
        //public async Task<ActionResult<GetStudentClassWithStudentLessonsDto?>> GetStudentLessonFromAClass(
        //    [FromRoute] long classId, [FromQuery] long studentId)
        //{
        //    return await _studentClassService.GetStudentClassWithStudentLessons(classId, studentId);
        //}
    }
}
