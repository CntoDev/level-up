CNTO_ROSTER_VERSION="0.5.0-snapshot"

sudo docker build -f container/Dockerfile.Database -t cntoarma/rooster-db:$CNTO_ROSTER_VERSION ./data/