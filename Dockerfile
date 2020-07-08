# STAGE 1 Build the .NET CORE Application
# here we use an image that contains the .NET CORE SDK
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY src/*.sln .
COPY src/Thinktecture.Docker.WebAPI/*.csproj ./Thinktecture.Docker.WebAPI/
RUN dotnet restore

COPY src/Thinktecture.Docker.WebAPI/. ./Thinktecture.Docker.WebAPI/

WORKDIR /app/Thinktecture.Docker.WebAPI
RUN dotnet publish -c Release -o out

# Now that we have compiled and published the app to /app/Thinktecture..../out 
# we can go ahead and use a fresh image as base...
# to run the application, we dont need the SDK, we just need the ASP.NET Core Runtime
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
# Copy the result of dotnet publish into our new image
COPY --from=build /app/Thinktecture.Docker.WebAPI/out ./

# Apply configuration values
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS="http://+:5000"
ENV APICONFIG__LIMIT=4

# expose the TCP port 5000
EXPOSE 5000
# run the application
ENTRYPOINT ["dotnet", "Thinktecture.Docker.WebAPI.dll"]