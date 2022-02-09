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

### Submitting feedbacks and bug reports

We use the [Issues section](https://github.com/CntoDev/rooster/issues) as _task list_ for what will be done next and in general to organize the development process.

The reference workflow is mentioned [in this CNTO Forum thread](https://www.carpenoctem.co/forums/m/26081621/viewthread/33652123-research-development-branch-101). If you find a bug, _and you are absolutely certain it's a bug_ feel free to open a new issue and label it as such. For feature request please open a [Discussion](https://github.com/CntoDev/rooster/discussions) under the category "Q&A" and label it with "feature request", ideally merging all feedback for a given session with an understandable name. Maintainers will decide which ideas to implement and open issues accordingly.

Thank you for your help! :)
