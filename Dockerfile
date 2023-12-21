# syntax=docker/dockerfile:1

FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /Lexplorer
COPY Lexplorer/Lexplorer.csproj ./Lexplorer/
COPY Shared/Shared.shproj ./Shared/
COPY Lexplorer.sln .
RUN dotnet restore Shared/Shared.shproj && \
    dotnet restore Lexplorer/Lexplorer.csproj
COPY Lexplorer ./Lexplorer
COPY Shared ./Shared

RUN dotnet publish Lexplorer/Lexplorer.csproj -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /publish
COPY --from=build-env /publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Lexplorer.dll"]
