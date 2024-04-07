using System.Diagnostics;
namespace frontendProgram;

public partial class LoginPage : ContentPage
{
    private static string githubLoginURL = "https://github.com/login/device";
    private static ProcessStartInfo psi = new ProcessStartInfo
    {
        FileName = githubLoginURL,
        UseShellExecute = true
    };


    public LoginPage(string partial_deivce_code, string full_device_code)
	{
		InitializeComponent();
        onLoad(partial_deivce_code,full_device_code);
    }

    async Task launchGithub()
    {
        await Task.Delay(5000);
        Process.Start(psi);
    }
    public async void onLoad(string partial_device_code, string full_device_code)
    {

        Label deviceCodeLabel = new Label();
        deviceCodeLabel.Text = "Your login code is: " + partial_device_code;
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
            Clipboard.SetTextAsync(partial_device_code);
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
            await Navigation.PopModalAsync();
        };
        LoginLayout.Children.Add(loggedIn);
        await launchGithub();


    }
}
