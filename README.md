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
dotnet run
```
This command will then start up the api with dev settings and it will be listening locally for instructions on the port shown in the terminal. You can then access the swagger endpoint documentation here: https://localhost:7187/swagger/index.html. Make sure that the port in the url matches the port shown in the terminal.

Once the api is running, you must then authenticate using a github token and a username. The api will then validate that the token sent is a valid token and will check if your username matches the username that github responds with. From then on, you will use the jwt given from the login response for all requests otherwise the api will reject the request.