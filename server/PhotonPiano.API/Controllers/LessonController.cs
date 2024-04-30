using Microsoft.AspNetCore.Mvc;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.Helper.Dtos.Lessons;
using PhotonPiano.Helper.Dtos.Locations;
using PhotonPiano.Helper.Dtos.Ultilities;

namespace PhotonPiano.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/[controller]")]
    public class LessonController : ControllerBase
    {
        private readonly ILessonSerivce _lessonService;
        private readonly IUtilities _utilities;

        public LessonController(ILessonSerivce lessonSerivce, IUtilities utilities)
        {
            _lessonService = lessonSerivce;
            _utilities = utilities;
        }
        [HttpGet("{year}/test")]
        public ActionResult<List<WeekDto>> GetLessons([FromRoute] int year)
        {
            return _utilities.GetAllWeeksInYear(year);
        }
        [HttpGet]
        public async Task<ActionResult<List<GetLessonWithLocationDto>>> GetLessons([FromQuery] QueryLessonDto queryLessonDto)
        {
            return await _lessonService.GetQueriedLessons(queryLessonDto);
        }
        [HttpPost]
        public async Task<ActionResult<GetLessonDto>> CreateLesson([FromBody] CreateLessonDto createLessonDto)
        {
            return await _lessonService.CreateLesson(createLessonDto);
        }
        [HttpPatch]
        public async Task<IActionResult> UpdateLesson([FromBody] UpdateLessonDto updateLessonDto)
        {
            await _lessonService.UpdateLesson(updateLessonDto);
            return NoContent();
        }
        [HttpPatch("auto-schedule")]
        public async Task<IActionResult> AutoScheduleAClass([FromBody] AutoArrangeLessonAClassDto autoArrangeLessonAClassDto)
        {
            await _lessonService.AutoScheduleAClass(autoArrangeLessonAClassDto);
            return Ok();
        }
        [HttpDelete("{lessonId}")]
        public async Task<IActionResult> DeleteLesson([FromRoute] long lessonId)
        {
            await _lessonService.DeleteLesson(lessonId);
            return Ok();
        }
        [HttpDelete("{classId}/clear")]
        public async Task<IActionResult> ClearNotStartedLessonOfAClass([FromRoute] long classId)
        {
            await _lessonService.ClearAllNotStartedLessonOfAClass(classId);
            return Ok();
        }
        [HttpDelete("clear-all")]
        public async Task<IActionResult> ClearNotStartedLessonOfAllClass()
        {
            await _lessonService.ClearAllNotStartedLessonOfAllClass();
            return Ok();
        }
    }
}
