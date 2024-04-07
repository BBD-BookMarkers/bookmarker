using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shared.Models
{
    [Table("Bookmark")]
    public class Bookmark
    {
        public int BookmarkId { get; set; } = default;
        public int UserId { get; set; }
        public required string Name { get; set; }
        [Column("createdDate")]
        public DateTime DateCreated { get; set; }
        public required Route Route { get; set; }
    }
}
