using Microsoft.Maui.Controls;
using System;
using System.Diagnostics;
using frontendProgram.Routes;
using frontendProgram.Bookmarks;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using frontendProgram.Requests;
namespace frontendProgram
{
    public partial class MainPage : ContentPage
    {
        private Dictionary<int,Bookmark> Bookmarks = new Dictionary<int,Bookmark>();
        public MainPage()
        {
            InitializeComponent();
            fillDictionary();
            loadBookmarks();


        }
        private void refreshList()
        {
            TableOfBookmarks.Children.Clear();
            loadBookmarks();
        }

        private void fillDictionary()
        {
            //This is a temporary function that just fills the dictionary with test data

            Random random = new Random();


            for (int i = 0; i < 50; i++)
            {
                int routeID = random.Next(1, 51);
                Route tempRoute = new Route(routeID, random.Next(1, 51), "C:\\Users\\bbdnet3169\\Desktop\\install the cli.txt");
                Bookmark tempBookmark = new Bookmark(i, random.Next(1, 51), routeID, "Cool as fuck for loop " + i.ToString(), DateTime.Parse("2024-04-06"), tempRoute);

                Bookmarks.Add(tempBookmark.BookmarkId, tempBookmark);
            }
        }
        private void loadBookmarks()
        {
            //this will call the backend to get all the bookmarks for a given user and then populate the ui with this info
            //for now it looks through the current array of items and adds them to the ui

            foreach (int bookmarkKey in Bookmarks.Keys)
            {
                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                var button = new Button
                {
                    Text = Bookmarks[bookmarkKey].Name,
                    TextColor = Color.Parse("Black"),
                    Margin = 2,
                    BackgroundColor = Color.FromArgb("#ac99ea"),
                    
                };
                button.Clicked += (s, e) =>
                {
                    openFilePath(s, e, bookmarkKey);
                };
                Grid.SetColumn(button, 0);

                var trashcanButton = new ImageButton
                {
                    Source = "trashcanimage.png",
                    HeightRequest = 40,
                    WidthRequest = 40,
                };

                trashcanButton.Clicked += (sender, e) =>
                {
                    Delete_Entry(sender, e, Bookmarks[bookmarkKey].BookmarkId);
                };
                Grid.SetColumn(trashcanButton, 1);

                grid.Children.Add(button);
                grid.Children.Add(trashcanButton);

                TableOfBookmarks.Children.Add(grid);
            }
        }

        private void openFilePath(object sender, EventArgs e, int bookmarkKey)
        {
            Bookmark bookmark = Bookmarks[bookmarkKey];
            int lineNumber=bookmark.Route.LineNumber;
            string filePath = bookmark.Route.FilePath;
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    Process.Start("notepad", filePath);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("file or editor does not exist");
            }
            Debug.WriteLine(sender);

        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            string full_device_code= await Request.GetDeviceCode();
            string partial_device_code= Request.getPartialDeviceCode(full_device_code);
            var popup= new LoginPage(partial_device_code,full_device_code);
            await Navigation.PushModalAsync(popup);
        }

        private void Delete_Entry(object sender, EventArgs e, int bookmarkID)
        {
            //eventually this will send a delete request to the database
            //for now it just deletes it from the array and refreshes the uii
            Bookmarks.Remove(bookmarkID);
            refreshList();
        }

        private void Refresh_Clicked(object sender, EventArgs e)
        {
            refreshList();
        }
    }

}
