#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Contact_Microservice/Contact_Microservice.csproj", "Contact_Microservice/"]
RUN dotnet restore "Contact_Microservice/Contact_Microservice.csproj"
COPY . .
WORKDIR "/src/Contact_Microservice"
RUN dotnet build "Contact_Microservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Contact_Microservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Contact_Microservice.dll"]