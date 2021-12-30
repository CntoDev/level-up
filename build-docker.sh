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
	docker image rm cnto-arma:$CNTO_ROSTER_VERSION
fi

# new build
dotnet publish src/Roster.Web -c Release -o docker-build
cp container/Dockerfile docker-build/Dockerfile
sudo docker build -t cntoarma/roster:$CNTO_ROSTER_VERSION ./docker-build
