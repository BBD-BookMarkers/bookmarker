using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToolWindow
{
    public static class DialogLauncher
    {
        public static string ShowDialog()
        {
            var window = new Window
            {
                Title = "Enter Bookmark Name",
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            var inputDialog = new NameInput();

            inputDialog.DialogResultChanged += (sender, dialogResult) =>
            {
                window.DialogResult = dialogResult; 
                window.Close(); 
            };

            window.Content = inputDialog;
            var result = window.ShowDialog();

            return result == true ? inputDialog.InputText : null;
        }
    }

}
