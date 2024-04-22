
using PhotonPiano.Helper.Dtos.Students;
using PhotonPiano.Models.Models;

namespace PhotonPiano.Helper.Dtos.EntranceTests
{
    public class GetEntranceTestWithStudent
    {
        public long Id { get; set; }

        public long? StudentId { get; set; }

        public decimal? BandScore { get; set; }

        public string? Rank { get; set; }

        public long? EntranceTestSlotId { get; set; }

        public int? Year { get; set; }

        public bool IsScoreAnnounced { get; set; }
        public virtual GetStudentWithUserDto Student { get; set; } = null!;
    }
}
