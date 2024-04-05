namespace Api.Models
{
    public class Route
    {
        public int RouteId { get; set; }
        public int LineNumber { get; set; }
        public required string FilePath { get; set; }
    }
}
