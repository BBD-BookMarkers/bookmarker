using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ToolWindow
{
    /// <summary>
    /// Interaction logic for ToolWindow1Control.
    /// </summary>
    public partial class ToolWindow1Control : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ToolWindow1Control"/> class.
        /// </summary>
        public ToolWindow1Control()
        {
            this.InitializeComponent();
            AddDynamicButtons();
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Sample code")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Default event handler naming pattern")]
        private void AddDynamicButtons()
        {
            string[] yourArray = { "1. MyForLoop 04/04/2024 19:45", "2. MyForLoop 04/04/2024 19:45", "3. MyForLoop 04/04/2024 19:45" };

            foreach (string item in yourArray)
            {
                Button dynamicButton = new Button
                {
                    // set the roundness of the button
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
            MessageBox.Show($"Navigating to Bookmark {clickedButton.Content}! (Not really)");
        }
    }
}