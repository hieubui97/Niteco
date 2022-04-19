#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["NitecoTest/NitecoTest.csproj", "NitecoTest/"]
RUN dotnet restore "NitecoTest/NitecoTest.csproj"
COPY . .
WORKDIR "/src/NitecoTest"
RUN dotnet build "NitecoTest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NitecoTest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NitecoTest.dll"]