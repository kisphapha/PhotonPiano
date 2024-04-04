using Mapster;
using PhotonPiano.BusinessLogic.Interfaces;
using PhotonPiano.DataAccess.Interfaces;
using PhotonPiano.Helper.Dtos.Criteria;
using PhotonPiano.Models.Enums;

namespace PhotonPiano.BusinessLogic.Services
{
    public class CriteriaSerivce : ICriteriaSerivce
    {
        private readonly ICriteriaRepository _criteriaRepository;
        public CriteriaSerivce(ICriteriaRepository criteriaRepository)
        {
            _criteriaRepository = criteriaRepository;
        }
        public async Task<List<GetCriteriaDto>> GetNormalCriteria() 
        {
            return (await _criteriaRepository.FindAsync(c => c.For == CriteriaFor.Normal.ToString())).Adapt<List<GetCriteriaDto>>();
        }
        public async Task<List<GetCriteriaDto>> GetEntranceTestCriteria()
        {
            return (await _criteriaRepository.FindAsync(c => c.For == CriteriaFor.Entrance.ToString())).Adapt<List<GetCriteriaDto>>();
        }
    }
}
