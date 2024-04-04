
namespace PhotonPiano.Helper.Dtos.EntranceTests
{
    public class GetEntranceTestResultDto
    {
        public long Id { get; set; }

        public long EntranceTestId { get; set; }

        public decimal? Score { get; set; }

        public long CriteriaId { get; set; }

        public int? Year { get; set; }
    }
}
