Remove-Item .\docker-build -Recurse -Force
dotnet publish --configuration Release --output .\docker-build
Copy-Item .\container\Dockerfile .\docker-build
Copy-Item .\container\entrypoint.sh .\docker-build
docker build -t cnto/identity:0.9 .\docker-build