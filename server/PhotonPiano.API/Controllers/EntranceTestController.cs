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
        [HttpGet("{id}/slot-detail")]
        public async Task<GetEntranceTestSlotDetailDto> GetEntranceTestByYear([FromRoute] long id)
        {
            return await _entranceTestSlotService.GetEntranceTestSlotDetail(id);
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
        public async Task<ActionResult<List<GetEntranceTestSlotWithLocationDto>>> GetEntranceTestSlot([FromQuery] int year)
        {
            return await _entranceTestSlotService.GetEntranceTestSlotsByYear(year);
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

        [HttpPatch("upsert-entrance-tests")]
        public async Task<IActionResult> AddEntranceTests([FromBody] AddStudentsToASlotDto addStudentsToASlotDto)
        {
            await _entranceTestSlotService.UpsertStudentsToEntranceTestSlot(addStudentsToASlotDto);
            return Ok();
        }

        [HttpPatch("{slotId}/announce")]
        public async Task<IActionResult> AnnounceEntranceTestSlot([FromRoute] long slotId)
        {
            await _entranceTestSlotService.AnnouceEntranceTestSlot(slotId);
            return Ok();
        }
        [HttpPatch("auto-accepting")]
        public async Task<IActionResult> AutoAcceptRegistrations([FromQuery] int number )
        {
            await _entranceTestService.AutoAcceptRegistrations(number);
            return Ok();
        }

        [HttpDelete("{slotId}")]
        public async Task<IActionResult> DeleteEntranceTestSlot([FromRoute] int slotId)
        {
            await _entranceTestSlotService.DeleteEntranceTestSlot(slotId);
            return Ok();
        }
    }
}
