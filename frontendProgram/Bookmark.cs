using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using frontendProgram.Routes;
namespace frontendProgram.Bookmarks
{

    public class Bookmark
    {
        private int bookmarkId;
        private int userId;
        private int routeId;
        private string name;
        private DateTime createdDate;
        private Route route;

        public Bookmark(int bookmarkId, int userId, int routeId, string name, DateTime createdDate, Route route)
        {
            this.bookmarkId = bookmarkId;
            this.userId = userId;
            this.routeId = routeId;
            this.name = name;
            this.createdDate = createdDate;
            this.route = route;
        }

        public int BookmarkId
        {
            get { return bookmarkId; }
            set { bookmarkId = value; }
        }

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public int RouteId
        {
            get { return routeId; }
            set { routeId = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        public Route Route
        {
            get { return route; }
            set { route = value; }
        }
    }
}
 
