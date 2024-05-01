namespace PhotonPiano.Helper.Dtos.Locations
{
    public class QueryLocationDto
    {
        public long? Id { get; set; }

        public string? Name { get; set; } 
        public string? Status { get; set; } 

        public long? CapacityFrom { get; set; }
        public long? CapacityTo { get; set; }
    }
}
