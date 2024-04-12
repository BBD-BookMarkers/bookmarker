using Microsoft.VisualStudio.PlatformUI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToolWindow
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : UserControl
    {
        private static string githubLoginURL = "https://github.com/login/device";
        public bool isLoggedIn { get; private set; }
        public event EventHandler<bool?> isLoggedInChanged;
        private static ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = githubLoginURL,
            UseShellExecute = true
        };
        public LoginPage(string user_code, string device_code)
        {
            InitializeComponent();
            onLoad(user_code, device_code);
        }

        public async void onLoad(string user_code, string device_code)
        {
            TextBlock deviceCodeLabel = new TextBlock();
            deviceCodeLabel.Text ="Your login code is: "+user_code +"\n Which has been added to your clipboard.";
            Clipboard.SetText(user_code);
            MainPanel.Children.Add(deviceCodeLabel);

            TextBlock extraInfo = new TextBlock();
            extraInfo.Text = "Once you have been authorized by Github, please click the button below.";
            MainPanel.Children.Add(extraInfo);

            Button loggedIn = new Button();
            loggedIn.Content = "I have logged in";
            loggedIn.Click += (s, e) =>
            {
                onLoggedInClicked(s,e, device_code);
            };
            MainPanel.Children.Add(loggedIn);
            await launchGithub();

        }
        async Task launchGithub()
        {
            await Task.Delay(5000);
            Process.Start(psi);
        }
        private async Task<string> finalizeLogin(string device_code)
        {
            string token = await Requests.AuthorizeLogin(device_code);
            if (token.Contains("access_token"))
            {
                return (token.Split('=')[1]);
            }
            else if (token.Contains("Login Error"))
            {
                return ("Not logged in");
            }
            else
            {
                return ("Fatal Error");
            }
        }
        private async void onLoggedInClicked(object sender, EventArgs e, string device_code)
        {
            string token = await finalizeLogin(device_code);

            if(token!= "Not logged in" && token!= "Fatal Error")
            {
                Requests.setBearerToken(token.Split('&')[0]);
                string user_name = await Requests.getUserName();
                await Requests.newJWT();
                isLoggedInChanged?.Invoke(this, true);
                isLoggedIn = true;
            }

        }
    }
}
