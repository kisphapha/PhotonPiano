namespace PhotonPiano.Helper.Dtos.EntranceTests
{
    public class GetEntranceTestDto
    {
        public long Id { get; set; }

        public long? StudentId { get; set; }

        public decimal? BandScore { get; set; }

        public string? Rank { get; set; }

        public string? ShortDesc { get; set; }

        public bool IsAnnouced { get; set; }

        public long? EntranceTestSlot { get; set; }

    }
}
