# YouTubeAPI
Simple API to Search and Store YouTube Results

# Features 
  1. Search YouTube Results by keywords in title, description
  2. Stores latest YouTube Results in the Database

# Built Using
  1. Swagger/Open API - For easy API access and testing
  2. Quartz - For robust asynchronous and scheduling
  3. ASP.NET CORE - Cross Platform development using C#
  4. Docker - For easy self-hosting
  5. SeriLog - For file logging


# How To Run

##Prerequisites
  1. Clone the repo on your local system
  2. Install the .NET CORE SDK (5.0) https://dotnet.microsoft.com/download/dotnet
  3. Use dot net sdk or docker to run the API

## You can run the API using DOT NET Core SDK by using the following command from the solution folder
  `dot net run`
  
   https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-run
  
## Alterantively, you can use the dockerfile contained in the solution folder
  `docker build -t YouTubeAPI .`
  
  `docker run -d -p 8080:80 --name myapp YouTubeAPI`
  
  https://docs.docker.com/samples/dotnetcore/#build-and-run-the-docker-image
  
## Testing
  You can access the API at  https://localhost:5001/swagger/index.html if running locally
  
  If using docker, the port will change
  
# Production Specific Things
  The code has in-memory database configured for testing purposes
  
  Actual Database can be linked by chaning the connection string in Startup.cs file
  
  https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/working-with-sql?view=aspnetcore-5.0&tabs=visual-studio

# App.settings
You can edit the following entries in the app.settings
  1. CheckRecent - If you want only the most recent results
  2. Delay - Defines how far back the API should search
  3. Keyword to Search - What keyword to use when querying the API
  4. API Key - YouTube API Key
  5. Search size - the number of results to query from YouTube at once
  6. Cron Expression - You can change the time when the async job executes ( Uses Cron Expression )
