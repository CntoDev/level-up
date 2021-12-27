using System.ComponentModel.Design.Serialization;
using Roster.DiscordService;
using MassTransit;
using Serilog;
using Microsoft.Extensions.Configuration;
using Roster.DiscordService.Configurations;

Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

Microsoft.Extensions.Hosting.IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMassTransit(x =>
        {
            IConfiguration configuration = hostContext.Configuration;

            services.AddSingleton(configuration.GetSection("DiscordOptions").Get<DiscordOptions>());

            services.AddScoped<DiscordService>();

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

            // Add consumers
            x.AddConsumer<MemberPromotedConsumer>();
        });

        services.AddMassTransitHostedService();
    })
    .UseSerilog()
    .Build();

await host.RunAsync();
