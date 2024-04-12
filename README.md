# bookmarker
Complete set of deliverables for the bookmarker project.

# Api
## Running the api locally
The api can be run with the following commands:
```bash
dotnet dev-certs https --trust
```
This command will possibly bring up a dialog to accept the certificate, choose yes.
Ensure that your dotnet user secrets are set up with the correct values. The two keys required are:

- Jwt:Key
- ConnectionStrings:DefaultConnection

Then run:
```bash
dotnet run
```
This command will then start up the api with dev settings and it will be listening locally for instructions on the port shown in the terminal. You can then access the swagger endpoint documentation here: https://localhost:7187/swagger/index.html. Make sure that the port in the url matches the port shown in the terminal.

Once the api is running, you must then authenticate using a github token and a username. The api will then validate that the token sent is a valid token and will check if your username matches the username that github responds with. From then on, you will use the jwt given from the login response for all requests otherwise the api will reject the request.

# Installation Process
## Extension
Download the Bookmarker.vsix. Double click the downloaded file, follow the prompt and run.

## Desktop Application
- Download the windows-x64.zip
- Extract the files
- Open the folder
- Right click on Install.ps1
- Click Run With Powershell
- Press R to run
- Press Enter to continue
- This brings up a popup asking if you want to run it in Powershell
- Powershell pops up and requests you to Run it again
- Press R and confirm by pressing Y
- Enter to continue
- Your application is now installed and you can look for the Bookmarker application on your windows system.

# Links to Jira and Confluence
- [Jira](https://beanpedia.atlassian.net/jira/software/projects/BOOK/boards/2)
- [Confluence](https://crafdb.atlassian.net/wiki/spaces/BKMK/overview)
