FROM microsoft/aspnet

COPY . /app
WORKDIR /app

RUN ["dnu", "restore"]

EXPOSE 8080

ENTRYPOINT ["dnx", "web"]