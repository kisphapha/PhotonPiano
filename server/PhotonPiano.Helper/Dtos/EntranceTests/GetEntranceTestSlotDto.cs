
namespace PhotonPiano.Helper.Dtos.EntranceTests
{
    public class GetEntranceTestSlotDto
    {
        public long Id { get; set; }

        public long LocationId { get; set; }

        public int Shift { get; set; }

        public DateOnly Date { get; set; }

        public bool isAnnouced { get; set; }
    }
}
