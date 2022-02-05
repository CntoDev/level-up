#/bin/bash

ROOSTER_DB=$(sudo docker ps --filter "name=dev-postgres" --format "{{.ID}}")

if [[ -n "$ROOSTER_DB" ]]; then
    echo "Stopping and removing old container..."
    sudo docker container stop dev-postgres
    sudo docker container rm dev-postgres
    echo "Old container stopped and removed."
fi

echo "Starting new Rooster database instance..."
sudo docker run --name dev-postgres -d -p 5432:5432 cntoarma/rooster-db:0.5.0-snapshot

ROOSTER_DB=$(sudo docker ps --filter "name=dev-postgres" --format "{{.ID}}")

if [[ -n "$ROOSTER_DB" ]]; then
    echo "New Rooster database instance started."
    echo "Username root@carpenoctem.co, password Admin*123"
fi