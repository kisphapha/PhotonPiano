using Microsoft.AspNetCore.Mvc;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.Helper.Dtos.EntranceTests;

namespace PhotonPiano.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/[controller]")]
    public class EntranceTestController : ControllerBase
    {
        private readonly IEntranceTestService _entranceTestService;
        private readonly IEntranceTestSlotService _entranceTestSlotService;

        public EntranceTestController(IEntranceTestService entranceTestService, IEntranceTestSlotService entranceTestSlotService)
        {
            _entranceTestService = entranceTestService;
            _entranceTestSlotService = entranceTestSlotService;
        }
        [HttpGet("{year}/year")]
        public async Task<List<GetEntranceTestDto>> GetEntranceTestByYear([FromRoute] int year)
        {
            return await _entranceTestService.GetEntranceTestsByYear(year);
        }
        [HttpGet("{studentId}/by-student")]
        public async Task<GetEntranceTestDto?> GetEntranceTestByStudentId([FromRoute] long studentId)
        {
            return await _entranceTestService.GetEntranceTestByStudentId(studentId,true);
        }
        [HttpGet("slots")]
        public async Task<ActionResult<List<GetEntranceTestSlotDto>>> GetPagedEntranceTestSlot([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            return await _entranceTestSlotService.GetPagedEntranceTestSlots(pageNumber, pageSize);
        }
        [HttpGet("{studentId}/entrance-test-score")]
        public async Task<ActionResult<GetEntranceTestWithResultDto>> GetEntranceTestScore([FromRoute] long studentId)
        {
            return await _entranceTestService.GetEntranceTestScoreOfAStudent(studentId);
        }
        [HttpPost]
        public async Task<ActionResult<GetEntranceTestDto>> CreateEntranceTest([FromBody] CreateEntranceTestDto createEntranceTestDto)
        {
            return await _entranceTestService.CreateEntranceTest(createEntranceTestDto);
        }
        [HttpPost("slot")]
        public async Task<ActionResult<GetEntranceTestSlotDto>> CreateEntranceTestSlot([FromBody] CreateEntranceTestSlotDto createEntranceTestSlotDto)
        {
            return await _entranceTestSlotService.CreateEntranceTestSlot(createEntranceTestSlotDto);
        }

        [HttpPatch("{slotId}/add-entrance-tests")]
        public async Task<IActionResult> AddEntranceTests([FromRoute] long slotId,[FromBody] AddEntranceTestToASlotDto addEntranceTestToASlotDto)
        {
            await _entranceTestSlotService.AddEntranceTestToEntranceTestSlot(addEntranceTestToASlotDto, slotId);
            return Ok();
        }

        [HttpPatch("{slotId}/announce")]
        public async Task<IActionResult> AnnounceEntranceTestSlot([FromRoute] long slotId)
        {
            await _entranceTestSlotService.AnnouceEntranceTestSlot(slotId);
            return Ok();
        }

    }
}
