FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine

COPY . /app
WORKDIR /app

RUN chmod +x ./entrypoint.sh

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["./entrypoint.sh"]