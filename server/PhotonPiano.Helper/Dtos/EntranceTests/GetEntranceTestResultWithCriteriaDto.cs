
using PhotonPiano.Helper.Dtos.Criteria;

namespace PhotonPiano.Helper.Dtos.EntranceTests
{
    public class GetEntranceTestResultWithCriteriaDto
    {
        public long Id { get; set; }

        public long EntranceTestId { get; set; }

        public decimal? Score { get; set; }

        public virtual GetCriteriaDto Criteria { get; set; } = null!;

    }
}
