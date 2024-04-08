using Swashbuckle.AspNetCore.Annotations;

namespace Shared.Models
{
    public class Route
    {
        [SwaggerSchema(ReadOnly = true)]
        public int RouteId { get; set; }
        public int LineNumber { get; set; }
        public required string FilePath { get; set; }
    }
}
