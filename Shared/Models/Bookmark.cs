using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace Shared.Models
{
    [Table("Bookmark")]
    public class Bookmark
    {
        [SwaggerSchema(ReadOnly = true)]
        public int BookmarkId { get; set; } = default;
        public int UserId { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public int RouteId { get; set; }
        public required string Name { get; set; }
        [Column("createdDate")]
        public DateTime DateCreated { get; set; }
        public required Route Route { get; set; }
    }
}
