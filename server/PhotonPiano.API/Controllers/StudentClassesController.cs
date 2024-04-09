using Microsoft.AspNetCore.Mvc;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.Helper.Dtos.Classes;
using PhotonPiano.Helper.Dtos.Lessons;
using PhotonPiano.Helper.Dtos.StudentClassTuitons;
using PhotonPiano.Helper.Dtos.StudentLessons;

namespace PhotonPiano.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/[controller]")]
    public class StudentClassesController : ControllerBase
    {
        private readonly IStudentLessonService _studentLessonService;
        private readonly IStudentClassTuitionService _studentClassTuitionService;

        public StudentClassesController(IStudentLessonService studentLessonService, IStudentClassTuitionService studentClassTuitionService)
        {
            _studentLessonService = studentLessonService;
            _studentClassTuitionService = studentClassTuitionService;
        }
        [HttpGet("{studentId}/get-lessons/{classId}")]
        public async Task<ActionResult<List<GetStudentLessonWithLocationDto>>> GetLessons(
            [FromRoute] long studentId, [FromRoute] long classId, [FromQuery] QueryLessonDto queryLessonDto)
        {
            return await _studentLessonService.GetDetailStudentLessonsByClassIdAndStudentId(studentId, classId, queryLessonDto);
        }
        [HttpGet("{studentId}/get-tuitions/{classId}")]
        public async Task<ActionResult<List<GetStudentClassTuitionDto>>> GetTuitions(
           [FromRoute] long studentId, [FromRoute] long classId)
        {
            return await _studentClassTuitionService.GetStudentClassTuitionsByStudentIdAndClassId(studentId, classId);
        }
    }
}
