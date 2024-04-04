
using PhotonPiano.Helper.Dtos.EntranceTests;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface IEntranceTestService
    {
        Task<GetEntranceTestDto> CreateEntranceTest(CreateEntranceTestDto createEntranceTestDto);
    }
}
