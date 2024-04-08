using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Shared.Models
{
    [Table("Route")]
    public class Route
    {
        public int RouteId { get; set; }
        public int LineNumber { get; set; }
        public required string FilePath { get; set; }
    }
}
