using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ToolWindow
{
    public static class LoginLauncher
    {
        public static bool ShowDialog(string user_code, string device_code)
        {
            var window = new Window
            {
                Title = "Login",
                SizeToContent =SizeToContent.WidthAndHeight,
                ResizeMode= ResizeMode.NoResize,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
            };

            
            var loginPage = new LoginPage(user_code,device_code);

            loginPage.isLoggedInChanged += (sender, dialogResult) =>
            {
                window.DialogResult = dialogResult;
                window.Close();
            };
            window.Content = loginPage;
            var result = window.ShowDialog();
            return result == true ? true : false;
        }
    }
}
