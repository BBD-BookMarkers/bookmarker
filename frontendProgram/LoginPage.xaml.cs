using Shared.Requests;
using System.Diagnostics;
namespace frontendProgram;

public partial class LoginPage : ContentPage
{
    private MessageService messageService;
    private static string githubLoginURL = "https://github.com/login/device";
    private static ProcessStartInfo psi = new ProcessStartInfo
    {
        FileName = githubLoginURL,
        UseShellExecute = true
    };


    public LoginPage(string user_code, string device_code)
	{
		InitializeComponent();
        this.messageService = new MessageService();
        onLoad(user_code,device_code);
    }

    async Task launchGithub()
    {
        await Task.Delay(5000);
        Process.Start(psi);
    }

    private async Task<string> finalizeLogin(string device_code)
    {
        string token= await Request.AuthorizeLogin(device_code);
        if (token.Contains("access_token"))
        {
            return (token.Split("=")[1]);
        }
        else if(token.Contains("Login Error"))
        {
            return ("Not logged in");
        }
        else
        {
            return ("Fatal Error");
        }
    }
    public async void onLoad(string user_code, string device_code)
    {

        Label deviceCodeLabel = new Label();
        deviceCodeLabel.Text = "Your login code is: " + user_code;
        deviceCodeLabel.TextColor = Color.Parse("lime");
        deviceCodeLabel.VerticalOptions = LayoutOptions.Center;
        deviceCodeLabel.HorizontalOptions = LayoutOptions.Center;

        ImageButton clipboard = new ImageButton
        {
            Source = "copyicon.png",
            BackgroundColor = Color.FromRgba(0, 0, 0, 0),
            WidthRequest = 1,
            HeightRequest = 1,
            MaximumWidthRequest = 1,
            Aspect=Aspect.AspectFit
        };

        clipboard.Clicked += (s, e) =>
        {
            Clipboard.SetTextAsync(user_code);
        };

        Grid grid= new Grid();
        grid.HorizontalOptions = LayoutOptions.Center;
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width=GridLength.Auto });

        Grid.SetColumn(deviceCodeLabel, 0);
        Grid.SetColumn(clipboard, 1);
        


        grid.Children.Add(deviceCodeLabel);
        grid.Children.Add(clipboard);
        LoginLayout.Children.Add(grid);

        Label extraInfo = new Label();
        extraInfo.Text = "Once you have been authorized by Github, please click the button below.";
        extraInfo.VerticalOptions = LayoutOptions.Center;
        extraInfo.HorizontalOptions = LayoutOptions.Center;
        extraInfo.FontSize = 25;
        LoginLayout.Children.Add(extraInfo);


        Button loggedIn = new Button();
        loggedIn.Margin = 10;
        loggedIn.Text = "I have logged in :)";


        loggedIn.Clicked += async (s, e) =>
        {
            string token = await finalizeLogin(device_code);
            if(token == "Fatal Error")
            {
                Label fail = new Label();
                fail.Text = "Fatal Error: Please try again later";
                fail.HorizontalOptions = LayoutOptions.Center;
                fail.FontSize = 25;
                fail.TextColor = Color.Parse("red");
                LoginLayout.Children.Add(fail);

            } else if(token == "Not logged in")
            {
                Label completeLogin = new Label();
                completeLogin.Text = "Please Complete Login First";
                completeLogin.HorizontalOptions = LayoutOptions.Center;
                completeLogin.FontSize = 25;
                LoginLayout.Children.Add(completeLogin);
            }
            else
            {
                messageService.sendMessage(token);
                Request.setBearerToken(token.Split("&")[0]);
                string user_name = await Request.getUserName();
                await Request.newJWT();
                await Navigation.PopModalAsync();
            }
        };

        Button backButton = new Button();
        backButton.Margin = 10;
        backButton.Text = "Back";

        backButton.Clicked += async (s, e) =>
        {
            await Navigation.PopModalAsync();
        };
        LoginLayout.Children.Add(loggedIn);
        LoginLayout.Children.Add(backButton);
        await launchGithub();


    }
}
