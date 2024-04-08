namespace PhotonPiano.Helper.Dtos.Locations
{
    public class GetLocationDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string Status { get; set; } = null!;

        public long Capacity { get; set; }
    }
}
