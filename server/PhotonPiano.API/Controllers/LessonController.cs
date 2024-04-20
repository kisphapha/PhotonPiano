using Microsoft.AspNetCore.Mvc;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.Helper.Dtos.Lessons;
using PhotonPiano.Helper.Dtos.Locations;

namespace PhotonPiano.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/[controller]")]
    public class LessonController : ControllerBase
    {
        private readonly ILessonSerivce _lessonService;

        public LessonController(ILessonSerivce lessonSerivce)
        {
            _lessonService = lessonSerivce;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetLessonWithLocationDto>>> GetLessons([FromQuery] QueryLessonDto queryLessonDto)
        {
            return await _lessonService.GetQueriedLessons(queryLessonDto);
        }


    }
}
