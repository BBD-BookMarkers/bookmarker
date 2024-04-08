using Api.Data;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Api.Repository
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly DataContext _context;

        public BookmarkRepository(DataContext context)
        {
            _context = context;
        }

        public Bookmark AddBookmark(Bookmark newBookmark)
        {
            _context.Bookmarks.Add(newBookmark);
            _context.SaveChanges();
            return newBookmark;
        }

        public Bookmark? DeleteBookmark(int userId, int bookmarkId)
        {
            Bookmark? bookmarkToDelete = _context.Bookmarks.Include(r => r.Route)
                .FirstOrDefault(b => b.UserId == userId && b.BookmarkId == bookmarkId);

            if (bookmarkToDelete == null)
            {
                return null;
            }
            _context.Remove(bookmarkToDelete.Route);
            _context.Remove(bookmarkToDelete);
            _context.SaveChanges();
            return bookmarkToDelete;
        }

        public ICollection<Bookmark> GetBookmarks(int userId)
        {
            return [.. _context.Bookmarks.Where(bookmark => bookmark.UserId == userId).Include(r => r.Route)];
        }
    }
}
