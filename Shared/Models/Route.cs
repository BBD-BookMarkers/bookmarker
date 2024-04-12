using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Models
{
    [Table("Route")]
    public class Route
    {
        [SwaggerSchema(ReadOnly = true)]
        public int RouteId { get; set; }
        public int LineNumber { get; set; }
        public required string FilePath { get; set; }

    }
}
