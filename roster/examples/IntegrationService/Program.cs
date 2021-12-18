using IntegrationService;
using Serilog;
using MassTransit;

Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

Microsoft.Extensions.Hosting.IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        // services.AddHostedService<Worker>();

        services.AddMassTransit(x =>
        {
            x.AddDelayedMessageScheduler();

            x.UsingRabbitMq((context, configurator) =>
            {
                configurator.UseDelayedMessageScheduler();

                configurator.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                configurator.ConfigureEndpoints(context);
            });

            // Add consumers & sagas
            x.AddConsumer<SampleConsumer>();            
        });

        services.AddMassTransitHostedService();
    })
    .UseSerilog()
    .Build();

await host.RunAsync();
