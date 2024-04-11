using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ToolWindow
{
    public partial class ToolWindow1Control : UserControl
    {

        private Dictionary<int, Bookmarker> Bookmark;
        public ToolWindow1Control()
        {
            this.InitializeComponent();
            AddDynamicButtons();
        }

        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]

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
                    Background = new System.Windows.Media.SolidColorBrush(Color.FromRgb(172, 153, 234)),
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