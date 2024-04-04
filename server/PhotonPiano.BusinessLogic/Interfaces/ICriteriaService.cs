

using PhotonPiano.Helper.Dtos.Criteria;

namespace PhotonPiano.BusinessLogic.Interfaces
{
    public interface ICriteriaSerivce
    {
        Task<List<GetCriteriaDto>> GetNormalCriteria();

        Task<List<GetCriteriaDto>> GetEntranceTestCriteria();
    }
}
