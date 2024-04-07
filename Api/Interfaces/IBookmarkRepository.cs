using Shared.Models;

namespace Api.Interfaces
{
    public interface IBookmarkRepository
    {
        ICollection<Bookmark> GetBookmarks(int userId);
        Bookmark? DeleteBookmark(int userId, int bookmarkId);
        Bookmark AddBookmark(Bookmark bookmark);
    }
}
