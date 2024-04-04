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

        public EntranceTestController(IEntranceTestService entranceTestService)
        {
            _entranceTestService = entranceTestService;
        }
        [HttpPost]
        public async Task<GetEntranceTestDto> CreateEntranceTest([FromBody] CreateEntranceTestDto createEntranceTestDto)
        {
            return await _entranceTestService.CreateEntranceTest(createEntranceTestDto);
        }
    }
}
