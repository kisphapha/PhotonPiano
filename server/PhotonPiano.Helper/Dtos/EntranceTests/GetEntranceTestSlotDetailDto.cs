
using PhotonPiano.Helper.Dtos.Locations;

namespace PhotonPiano.Helper.Dtos.EntranceTests
{
    public class GetEntranceTestSlotDetailDto
    {
        public long Id { get; set; }

        public long LocationId { get; set; }

        public int Shift { get; set; }

        public DateOnly Date { get; set; }

        public bool IsAnnouced { get; set; }

        public DateTime? AnnounceTime { get; set; }

        public GetLocationDto Location { get; set; } = null!;

        public ICollection<GetEntranceTestWithStudent> EntranceTests { get; set; } = new List<GetEntranceTestWithStudent>();
    }
}
