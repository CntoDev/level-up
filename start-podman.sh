#!/bin/bash

ROOSTER_VERSION=0.5.0
ROOSTER_DB=$(podman ps -a --filter "name=dev-postgres" --format "{{.ID}}")
ROOSTER_MESSAGE_BUS=$(podman ps -a --filter "name=rooster-message-broker" --format "{{.ID}}")

if [[ -n "$ROOSTER_DB" ]]; then
    echo "Stopping and removing old container..."
    podman container stop dev-postgres
    podman container rm dev-postgres
    echo "Old container stopped and removed."
fi

echo "Starting new Rooster database instance..."
podman run --name dev-postgres -d -p 5432:5432 docker.io/cntoarma/rooster-db:$ROOSTER_VERSION

ROOSTER_DB=$(podman ps --filter "name=dev-postgres" --format "{{.ID}}")

if [[ -n "$ROOSTER_DB" ]]; then
    echo "New Rooster database instance started."
    echo "Username root@carpenoctem.co, password Admin*123"
fi

if [[ -n "$ROOSTER_MESSAGE_BUS" ]]; then
    echo "Stopping and removing old message bus..."
    podman container stop rooster-message-broker
    podman container rm rooster-message-broker
    echo "Old container stopped and removed."
fi

echo "Starting new Rooster message broker..."
podman run --name rooster-message-broker -d -p 5672:5672 -p 15672:15672 docker.io/masstransit/rabbitmq

ROOSTER_MESSAGE_BUS=$(podman ps --filter "name=rooster-message-broker" --format "{{.ID}}")

if [[ -n "$ROOSTER_MESSAGE_BUS" ]]; then
    echo "New Rooster message bus instance started."
fi
