using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace Shared.Models
{
    public class Bookmark
    {
        [SwaggerSchema(ReadOnly = true)]
        public int BookmarkId { get; set; } = default;
        public int UserId { get; set; }
        public int RouteId { get; set; }
        public required string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public required Route Route { get; set; }
    }
}
