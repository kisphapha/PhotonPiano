
using PhotonPiano.Helper.Dtos.EntranceTests;

namespace PhotonPiano.Helper.Dtos.EntranceTest
{
    public class GetEntranceTestScoreDto
    {
        public decimal? BandScore { get; set; }

        public string? Rank { get; set; }

        public int? Year { get; set; }
        public GetEntranceTestSlotWithLocationDto? EntranceTestSlot { get; set; }


    }
}
