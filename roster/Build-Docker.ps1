if (Test-Path .\docker-build) {
	Remove-Item .\docker-build -Recurse -Force
}

dotnet publish .\src\Roster.Web --configuration Release --output .\docker-build
Copy-Item .\container\Dockerfile .\docker-build
docker build -t cntoarma/roster:0.2.0-rc5 -t cntoarma/roster:latest .\docker-build
