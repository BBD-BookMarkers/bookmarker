namespace ToolWindow
{
    public partial class ToolWindow1Control : UserControl
    {

        private Dictionary<int, Bookmark> Bookmark;
        public ToolWindow1Control()
        {
            this.InitializeComponent();
            AddDynamicButtons();
        }

        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        
        private async void Login_Clicked(object sender, EventArgs e)
        {
            string full_device_code = await Request.GetDeviceCode();
            string user_code = Request.getUserCode(full_device_code);
            string device_code = Request.getDeviceCode(full_device_code);
            var popup = new LoginPage(user_code, device_code);
            await Navigation.PushModalAsync(popup);
            // Display the screen with the list of bookmarks

        }

        private void AddDynamicButtons()
        {
            string[] yourArray = { "1. MyForLoop 04/04/2024 19:45", "2. MyForLoop 04/04/2024 19:45", "3. MyForLoop 04/04/2024 19:45" };

            foreach (string item in yourArray)
            {
                Button dynamicButton = new Button
                {
                    BorderThickness = new Thickness(0),
                    Content = item,
                    Width = 300,
                    Height = 50,
                    Margin = new Thickness(5),
                    Background = new SolidColorBrush(Color.FromRgb(172, 153, 234)),
                    BorderBrush = new SolidColorBrush(Color.FromRgb(172, 153, 234)),
                };

                dynamicButton.Click += DynamicButton_Click;
                this.DynamicButtonStackPanel.Children.Add(dynamicButton);

            }
        }
        private void DynamicButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            //TODO: Add code to handle navigating to file
        }
    }
}