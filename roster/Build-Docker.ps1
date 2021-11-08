if (Test-Path .\docker-build) {
	Remove-Item .\docker-build -Recurse -Force
}

dotnet publish .\src\Roster.Web --configuration Release --output .\docker-build
Copy-Item .\container\Dockerfile .\docker-build
docker build -t cntoarma/roster:0.1.0 .\docker-build