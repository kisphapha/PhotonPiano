
using PhotonPiano.Helper.Dtos.Instructors;
using PhotonPiano.Helper.Dtos.Locations;
using PhotonPiano.Models.Models;

namespace PhotonPiano.Helper.Dtos.EntranceTests
{
    public class GetEntranceTestSlotDetailDto
    {
        public long Id { get; set; }

        public long LocationId { get; set; }

        public int Shift { get; set; }

        public DateOnly Date { get; set; }

        public bool IsAnnoucedTime { get; set; }

        public DateTime? AnnounceTime { get; set; }

        public bool IsAnnoucedScore { get; set; }

        public GetInstructorWithUserDto? Instructor { get; set; }

        public GetLocationDto Location { get; set; } = null!;

        public ICollection<GetEntranceTestWithStudent> EntranceTests { get; set; } = new List<GetEntranceTestWithStudent>();
    }
}
