using System.ComponentModel.DataAnnotations.Schema;

namespace Api.Models
{
    [Table("user")]
    public class User
    {
        public int UserId { set; get; }
        public required string Username { set; get; }
        public ICollection<Bookmark> Bookmarks { set; get; } = null!;
    }
}
