
using PhotonPiano.Helper.Dtos.EntranceTests;
using PhotonPiano.Models.Models;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface IEntranceTestService
    {
        Task<GetEntranceTestDto> CreateEntranceTest(CreateEntranceTestDto createEntranceTestDto);

        Task<GetEntranceTestDto?> GetEntranceTestByStudentId(long studentId, bool isRequired);

        Task<EntranceTest> GetEntranceTestByRequiredId(long id);

        Task UpdateEntranceTestId(long id, long? slotId);

        Task<GetEntranceTestWithResultDto> GetEntranceTestScoreOfAStudent(long studentId);

        Task<List<GetEntranceTestDto>> GetEntranceTestsByYear(int year);

        Task AutoAcceptRegistrations(int number);

        Task<EntranceTest?> GetEntranceTestByStudentIdAndYear(long studentId, int year);
    }
}
