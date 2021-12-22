docker container stop cnto-roster
docker container rm cnto-roster
docker run --name cnto-roster -p 80:80 -e RabbitMq__Host=host.docker.internal -e RabbitMq__Username=guest -e RabbitMq__Password=guest -e ConnectionStrings__Roster="Host=host.docker.internal;Database=roster;Username=postgres;Password=cnto_dev" -e ConnectionStrings__PostgresConnection="Host=host.docker.internal;Database=identity;Username=postgres;Password=cnto_dev" -d cntoarma/roster:0.4.0
