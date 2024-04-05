namespace Api.Models
{
    public class Bookmark
    {
        public int BookmarkId { get; set; }
        public int UserId { get; set; }
        public int RouteId { get; set; }
        public required string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public required Route Route { get; set; }
    }
}
