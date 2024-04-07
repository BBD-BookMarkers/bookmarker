# bookmarker
Complete set of deliverables for the bookmarker project.

# Api
## Running the api locally
The api can be run with the following commands:
```bash
dotnet dev-certs https --trust
```
This command will possibly bring up a dialog to accept the certificate, choose yes.
Then run:
```bash
dotnet run --launch-profile https
```
This command will then start up the api and it will be listening for instructions on the port shown in the terminal. You can then access the swagger endpoint documentation here: https://localhost:7187/swagger/index.html. Make sure that the port in the url matches the port shown in the terminal.
