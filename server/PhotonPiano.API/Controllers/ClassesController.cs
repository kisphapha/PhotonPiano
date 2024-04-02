using Microsoft.AspNetCore.Mvc;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.BusinessLogic.Services;
using PhotonPiano.Helper.Dtos.StudentClasses;
using PhotonPiano.Helper.Dtos.Students;

namespace PhotonPiano.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/[controller]")]
    public class ClassesController : ControllerBase
    {
        private readonly IStudentClassService _studentClassService;

        public ClassesController(IStudentClassService studentClassService)
        {
            _studentClassService = studentClassService;
        }

        //[HttpGet("{classId}/student-lessons")]
        //public async Task<ActionResult<GetStudentClassWithStudentLessonsDto?>> GetStudentLessonFromAClass(
        //    [FromRoute] long classId, [FromQuery] long studentId)
        //{
        //    return await _studentClassService.GetStudentClassWithStudentLessons(classId, studentId);
        //}
    }
}
