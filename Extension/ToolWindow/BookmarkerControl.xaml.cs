using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ToolWindow
{
    public partial class ToolWindow1Control : UserControl
    {

        private Dictionary<int, Bookmarks> allBookmarks;
        public ToolWindow1Control()
        {
            this.InitializeComponent();
            //AddDynamicButtons();
        }

        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        //
        private async Task getBookmarks()
        {

            allBookmarks = await Requests.getBookmarks();
        }
        private void AddDynamicButtons()
        {
            DynamicButtonStackPanel.Children.Clear();


            foreach (int key in allBookmarks.Keys)
            {
                Button dynamicButton = new Button
                {
                    BorderThickness = new Thickness(0),
                    Content = allBookmarks[key].DateCreated.ToString("yyyy-MM-dd") + " " + allBookmarks[key].Name + " Line Number: " + allBookmarks[key].Route.LineNumber,
                    Width = 450,
                    Height = 50,
                    Margin = new Thickness(5),
                    Background = new System.Windows.Media.SolidColorBrush(Color.FromRgb(172, 153, 234)),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(172, 153, 234)),
                    HorizontalContentAlignment=HorizontalAlignment.Left,
                    Padding = new Thickness(5),
                    
                };

                dynamicButton.Click +=( s,e) =>{
                    DynamicButton_Click(s,e,key);
                };
                    
                   
                this.DynamicButtonStackPanel.Children.Add(dynamicButton);

            }
        }
        private void DynamicButton_Click(object sender, RoutedEventArgs e,int bookmarkKey)
        {
            Bookmarks bookmark = allBookmarks[bookmarkKey];
            int lineNumber = bookmark.Route.LineNumber;
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
        }

        private async void Refresh_Clicked(object sender, RoutedEventArgs e)
        {
            await getBookmarks();
            AddDynamicButtons();
        }
        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            string full_device_code = await Requests.GetDeviceCode();
            string user_code = Requests.getUserCode(full_device_code);
            string device_code =Requests.getDeviceCode(full_device_code);
            bool result = LoginLauncher.ShowDialog(user_code,device_code);
            if (result)
            {
                Debug.WriteLine("I ran");
                await getBookmarks();
                AddDynamicButtons();
            }
            
            
        }
    }
}