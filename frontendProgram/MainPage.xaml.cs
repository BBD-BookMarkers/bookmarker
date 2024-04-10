using Microsoft.Maui.Controls;
using System;
using System.Diagnostics;
using Shared.Models;
using System.Xml.Linq;
using Microsoft.Extensions.Logging;
using Shared.Requests;
namespace frontendProgram
{
    public partial class MainPage : ContentPage
    {
        private Dictionary<int, Bookmark> Bookmarks;
        private MessageService MessageService;
        private string bearertoken;
        public MainPage()
        {
            InitializeComponent();
            


        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await fillDictionary();
            checkLogin();
            loadBookmarks();
        }
        private void checkLogin()
        {
            if(Request.getUsername() != null)
            {
                welcomeMessage.Text = "Welcome "+Request.getUsername();
            }
            if (Request.getBearerToken() != null)
            {
                Login.Text = "Logout";
                Login.Clicked -= Login_Clicked;
                Login.Clicked += logout;
            }
        }

        private async void logout(object sender, EventArgs e)
        {
            Login.Clicked -= logout;
            Login.Text = "Login";
            welcomeMessage.Text = "Welcome";
            Request.setUsername(null);
            Request.setBearerToken(null);
            Request.setJWT(null);
            Login.Clicked += Login_Clicked;
            await refreshList();
        }
        private async Task refreshList()
        {
            await fillDictionary();
            TableOfBookmarks.Children.Clear();
            loadBookmarks();
        }

        private async Task fillDictionary()
        {
            Bookmarks= new Dictionary<int, Bookmark>();
            if(Request.getJWT() != null)
            {
                Bookmarks = await Request.getBookmarks();
            }
            
          
        }
        private void loadBookmarks()
        {

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
            string user_code= Request.getUserCode(full_device_code);
            string device_code=Request.getDeviceCode(full_device_code);
            var popup= new LoginPage(user_code,device_code);
            await Navigation.PushModalAsync(popup);
        }

        private async void Delete_Entry(object sender, EventArgs e, int bookmarkID)
        {
            await Request.deleteBookmark(bookmarkID);
            await refreshList();
        }

        private async void Refresh_Clicked(object sender, EventArgs e)
        {
           await refreshList();
        }
    }

}
