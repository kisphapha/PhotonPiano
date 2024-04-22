using PhotonPiano.Models.Models;

namespace PhotonPiano.Helper.Dtos.EntranceTests
{
    public class GetEntranceTestWithResultDto
    {
        public long Id { get; set; }

        public long? StudentId { get; set; }

        public decimal? BandScore { get; set; }

        public string? Rank { get; set; }

        public string? ShortDesc { get; set; }

        public int? Year { get; set; }

        public bool IsScoreAnnounced { get; set; }

        public GetEntranceTestSlotDto? EntranceTestSlot { get; set; }

        public ICollection<GetEntranceTestResultWithCriteriaDto> EntranceTestResults { get; set; } = new List<GetEntranceTestResultWithCriteriaDto>();


    }
}
