# Cleaning up
if (Test-Path .\docker-build) {
	Remove-Item .\docker-build -Recurse -Force
}

docker container stop cnto-roster
docker container rm cnto-roster
docker image rm cntoarma/roster:0.4.0-snapshot

# Build new image
dotnet publish .\src\Roster.Web --configuration Release --output .\docker-build
Copy-Item .\container\Dockerfile .\docker-build
docker build -t cntoarma/roster:0.4.0-snapshot .\docker-build
