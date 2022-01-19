#!/bin/bash

if [[ -d docker-build ]]; then
	echo "Removing docker build..."
	rm -rf docker-build
fi

mkdir docker-build

CNTO_ROSTER_VERSION="0.5.0-snapshot"
CNTO_ROSTER_CONTAINER=$(sudo docker ps -f name=cnto-arma --format '{{.Names}}')

if [[ -n "$CNTO_ROSTER_CONTAINER" ]]; then
	echo "Stopping and removing cnto-arma container..."
	sudo docker container stop cnto-arma; \
	docker container rm cnto-arma; \
	docker image rm cntoarma/rooster:$CNTO_ROSTER_VERSION

	echo "Stopping and removing cnto-arma-discord container"
	sudo docker container stop cnto-arma-discord; \
	docker container rm cnto-arma-discord; \
	docker image rm cntoarma/rooster-discord:$CNTO_ROSTER_VERSION
fi

# build core service
echo "Building Rooster core service..."
dotnet publish src/Roster.Web -c Release -o docker-build
sudo docker build -f container/Dockerfile -t cntoarma/rooster:$CNTO_ROSTER_VERSION ./docker-build
echo "Rooster core service built."

# clear build folder
rm -rf docker-build/*

# build discord service
echo "Building Rooster Discord service..."
dotnet publish src/Roster.DiscordService -c Release -o docker-build
sudo docker build -f container/Dockerfile-DiscordService -t cntoarma/rooster-discord:$CNTO_ROSTER_VERSION ./docker-build
echo "Rooster Discord service built."

# tidy after build
rm -rf docker-build
