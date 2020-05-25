#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Pull down an image from Docker Hub that includes only the ASP.NET core runtime:
# https://hub.docker.com/_/microsoft-dotnet-core-aspnet/
# We don't need the SDK anymore, so this will produce a lighter-weight image
# that can still run the app.
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Pull down an image from Docker Hub that includes the .NET core SDK: 
# https://hub.docker.com/_/microsoft-dotnet-core-sdk
# This is so we have all the tools necessary to compile the app.
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

# Fetch and install Node 10. Make sure to include the --yes parameter 
# to automatically accept prompts during install, or it'll fail.
RUN curl --silent --location https://deb.nodesource.com/setup_10.x | bash -
RUN apt-get install --yes nodejs

# Copy the source from your machine onto the container.
WORKDIR /src
COPY ["TechChallenge.WebApi/TechChallenge.WebApi.csproj", "TechChallenge.WebApi/"]
RUN dotnet restore "TechChallenge.WebApi/TechChallenge.WebApi.csproj"
COPY . .
WORKDIR "/src/TechChallenge.WebApi"
RUN dotnet build "TechChallenge.WebApi.csproj" -c Release -o /app/build

# Compile, then pack the compiled app and dependencies into a deployable unit.
FROM build AS publish
RUN dotnet publish "TechChallenge.WebApi.csproj" -c Release -o /app/publish

# Copy the published app to this new runtime-only container.
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# To run the app, run `dotnet sample-app.dll`, which we just copied over.
ENTRYPOINT ["dotnet", "TechChallenge.WebApi.dll"]