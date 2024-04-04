using PhotonPiano.Helper.Dtos.EntranceTests;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface IEntranceTestResultService
    {
        Task<GetEntranceTestResultDto> CreateEntranceTestResult(CreateEntranceTestResultDto createEntranceTestResultDto);
    }
}
