FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 3001

ENV DOTNET_CONNECTION_STRING="Server=db; Database=students_db; Uid=root; Pwd=123456;"
ENV ASPNETCORE_URLS=http://+:3001

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["back-end.Web.csproj", "./"]
RUN dotnet restore "back-end.Web.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "back-end.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "back-end.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "back-end.Web.dll"]
