﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ToolWindow
{
    public class Bookmark
    {
        private ArrayList bookmarks = new ArrayList();

        public Bookmark CreateBookmark(int userId, string name, int lineNumber, string filePath)
        {
            // Creating a new Route instance
            //var route = new Route
            //{
                //LineNumber = lineNumber,
                //FilePath = filePath
            //};

            // Creating a new Bookmark instance
            // Adding the bookmark to the ArrayList
            // bookmarks.Add(bookmark);
            // return bookmark;

            return null;
        }

        public ArrayList GetAllBookmarks()
        {
            return bookmarks;
        }
    }
}
