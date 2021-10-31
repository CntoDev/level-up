FROM mcr.microsoft.com/dotnet/aspnet:5.0

COPY . /app
WORKDIR /app

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Roster.Web.dll"]