using Mapster;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.EntranceTests;
using PhotonPiano.Models.Models;

namespace PhotonPiano.BusinessLogic.Services
{
    public class EntranceTestResultSerivce : IEntranceTestResultService
    {
        private readonly IEntranceTestResultRepository _entranceTestResultRepository;
        public EntranceTestResultSerivce(IEntranceTestResultRepository entranceTestResultRepository)
        {
            _entranceTestResultRepository = entranceTestResultRepository;
        }

        public async Task<GetEntranceTestResultDto> CreateEntranceTestResult(CreateEntranceTestResultDto createEntranceTestResultDto)
        {
            return (await _entranceTestResultRepository
                .AddAsync(createEntranceTestResultDto.Adapt<EntranceTestResult>()))
                .Adapt<GetEntranceTestResultDto>();
        }
    }
}
