namespace PhotonPiano.Helper.Dtos.Criteria
{
    public class GetCriteriaDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public decimal Weight { get; set; }

        public string? Description { get; set; }

        public string? For { get; set; }
    }
}
