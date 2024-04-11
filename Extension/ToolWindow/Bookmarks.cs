using System;
namespace ToolWindow
{
    public class Bookmarks
    {
        public int BookmarkId { get; set; } = default;
        public int UserId { get; set; }
        public int RouteId { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public Routes Route { get; set; }
    }
}
