# Carpe Noctem Tactical Operations - Community Roster System

CNTO Tool for member management.

## Getting started

Use `docker-compose` to start both the production and development environment. For both envs a compose file is available in the repository alongisde with a `.env` stub configuration and `.env.dev` for development scenario.

### Development

The development environment defines the application container, message broker and database. To use it, run

`docker-compose -f docker-compose.dev.yml --env-file .env.dev up -d`

All the configuration parameters are contained in the `.env.dev`.

### Production

The production environment only defines application and message broker, as it is recommended to run database on bare metal. To deploy, make a copy of `.env` named `.env.prod` and fill in the `<VALUE>` placeholders with actual values then run:

`docker-compose --env-file .env.prod up -d`

For more details on how to run Rooster, [checkout the Wiki](https://github.com/CntoDev/rooster/wiki/Getting-started).

## Contributing

- Branch `master` is for production-ready code and cannot be pushed to. Open a PR and code will be merged into `master` once it's approved.
  - _Note_: rule will be enforced once project reaches version `1.0.0`.
- Branch `develop` is for working on next release. That's the place to branch off from when working on new features or non-critical bug fixing.
- In general, the project follows [this](https://nvie.com/posts/a-successful-git-branching-model/) git flow with some tweaking:
  - During initial development phase, `release` branches can be skipped and code can be merged directly from `develop` into `master`.
