using Shared.Models;
using Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkRepository _bookmarkRepository;
        public BookmarkController(IBookmarkRepository bookmarkRepository)
        {
            _bookmarkRepository = bookmarkRepository;
        }

        private int GetLoggedInUserId()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            var userId = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                throw new Exception("Cannot find userId within token");
            }

            return int.Parse(userId);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Bookmark))]
        public IActionResult AddBookmark(Bookmark bookmark)
        {
            bookmark.UserId = GetLoggedInUserId();
            Bookmark newBookmark = _bookmarkRepository.AddBookmark(bookmark);
            return Ok(newBookmark);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Bookmark>))]
        public IActionResult GetBookmarks()
        {
            int userId = GetLoggedInUserId();
            var bookmarks = _bookmarkRepository.GetBookmarks(userId);

            return Ok(bookmarks);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        public IActionResult DeleteBookmark(int bookmarkId)
        {
            int userId = GetLoggedInUserId();
            Bookmark? deletedBookmark = _bookmarkRepository.DeleteBookmark(userId, bookmarkId);

            if (deletedBookmark == null)
            {
                return NotFound("Bookmark not found for current user");
            }

            return Ok(deletedBookmark);
        }
    }
}