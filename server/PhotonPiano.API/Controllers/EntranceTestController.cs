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

        [HttpGet("{studentId}/by-student")]
        public async Task<GetEntranceTestDto?> GetEntranceTestByStudentId([FromRoute] long studentId)
        {
            return await _entranceTestService.GetEntranceTestByStudentId(studentId,true);
        }
        [HttpGet("slots")]
        public async Task<List<GetEntranceTestSlotDto>> GetPagedEntranceTestSlot([FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            return await _entranceTestSlotService.GetPagedEntranceTestSlots(pageNumber, pageSize);
        }
        [HttpPost]
        public async Task<GetEntranceTestDto> CreateEntranceTest([FromBody] CreateEntranceTestDto createEntranceTestDto)
        {
            return await _entranceTestService.CreateEntranceTest(createEntranceTestDto);
        }
        [HttpPost("slot")]
        public async Task<GetEntranceTestSlotDto> CreateEntranceTestSlot([FromBody] CreateEntranceTestSlotDto createEntranceTestSlotDto)
        {
            return await _entranceTestSlotService.CreateEntranceTestSlot(createEntranceTestSlotDto);
        }
    }
}
