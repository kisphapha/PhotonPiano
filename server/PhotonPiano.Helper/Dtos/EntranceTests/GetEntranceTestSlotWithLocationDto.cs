﻿
using PhotonPiano.Helper.Dtos.Locations;
using PhotonPiano.Models.Models;

namespace PhotonPiano.Helper.Dtos.EntranceTests
{
    public class GetEntranceTestSlotWithLocationDto
    {
        public long Id { get; set; }

        public long LocationId { get; set; }

        public int Shift { get; set; }

        public DateOnly Date { get; set; }

        public bool IsAnnoucedTime { get; set; }

        public DateTime? AnnounceTime { get; set; }

        public bool IsAnnoucedScore { get; set; }

        public GetLocationDto Location { get; set; } = null!;
    }
}
