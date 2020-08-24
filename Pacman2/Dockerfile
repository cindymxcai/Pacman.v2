## Docker multi-stage build file
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build 
CMD ["dotnet", "--version"]
WORKDIR /src 
COPY ["Pacman2/Pacman2.csproj", "Pacman2/"]
# Restore dependencies
RUN dotnet restore "Pacman2/Pacman2.csproj" 
COPY . . 
WORKDIR "/src/Pacman2" 
# Remove bin/ and obj/ folders
RUN find -type d -name bin -prune -exec rm -rf {} \; && find -type d -name obj -prune -exec rm -rf {} \;
RUN dotnet build "Pacman2.csproj" -c Release -o /app/build 
 
FROM build AS package 
RUN dotnet publish "Pacman2.csproj" -c Release -o /app/publish 
 
# Setup runtime
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS final 
WORKDIR /app 
COPY --from=package /app/publish . 
ENTRYPOINT ["dotnet", "Pacman2.dll"]
