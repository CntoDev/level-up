using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Roster.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Roster.Core.Services;
using Roster.Core.Storage;
using Roster.Infrastructure.Storage;
using MassTransit;
using Roster.Infrastructure.Consumers;
using Roster.Core.Events;
using Roster.Infrastructure.Events;
using Roster.Infrastructure.Configurations;
using Roster.Infrastructure;
using Serilog;
using Roster.Web.Security;
using Microsoft.AspNetCore.HttpOverrides;
using Roster.Core.Consumers;
using Roster.Core.Domain;
using Roster.Core.Sagas;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using Microsoft.Extensions.Logging;

namespace Roster.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Logging startup info
            Log.Information("Configuring services, UTC time is {utcTime}.", DateTime.UtcNow);
            Log.Information("ConnectionStrings {roster} {postgres}", Configuration.GetConnectionString("Roster"), Configuration.GetConnectionString("PostgresConnection"));
            Log.Information("MailJet {@mailjetOptions}", Configuration.GetSection("MailJet").Get<MailJetOptions>());
            Log.Information("RabbitMq {@rabbitmqOptions}", Configuration.GetSection("RabbitMq").Get<RabbitMqOptions>());
            Log.Information("Recruitment {@recruitment}", Configuration.GetSection("Recruitment").Get<RecruitmentSettings>());
            #endregion

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("PostgresConnection")));
            services.AddDbContext<RosterDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("Roster"), o => o.MigrationsAssembly("Roster.Web")));
            services.AddDbContext<ProcessDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("Roster"), o => o.MigrationsAssembly("Roster.Web")));

            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();

            // Identity
            services.AddSingleton<IEmailSender, EmailService>();

            // Roster Core registrations here
            services.AddScoped<IQuerySource, RosterDbContext>();
            services.AddScoped<IProcessSource, ProcessDbContext>();
            services.AddScoped(typeof(IStorage<>), typeof(Storage<>));

            services.AddScoped<IDiscordValidationService, DummyDiscordValidationService>();
            services.AddScoped<ApplicationFormService>();
            services.AddScoped<MemberService>();

            // MailJet registrations
            services.AddSingleton(sp => Configuration.GetSection("MailJet").Get<MailJetOptions>());

            services.AddSingleton<EmailService>();

            // Add Mass Transit
            RabbitMqOptions rabbitMqOptions = Configuration.GetSection("RabbitMq").Get<RabbitMqOptions>();

            // Application settings
            RecruitmentSettings recruitmentSettings = Configuration.GetSection("Recruitment").Get<RecruitmentSettings>();

            services.AddMassTransit(x =>
            {
                x.AddDelayedMessageScheduler();

                // Add consumers
                x.AddConsumer<MemberCreationConsumer>();
                x.AddConsumer<EmailSender>();
                x.AddConsumer<PromotionConsumer>();                
                x.AddConsumer<DischargeConsumer>();
                x.AddConsumer<WarningsConsumer>();
                
                // Add sagas
                x.AddSaga<RecruitmentSaga>().EntityFrameworkRepository(r =>
                {
                    r.ExistingDbContext<ProcessDbContext>();
                    r.LockStatementProvider = new LockStatementProvider();
                });

                // send endpoint conventions
                EndpointConvention.Map<DischargeRecruit>(new Uri("queue:discharge-processing"));

                x.UsingRabbitMq((context, configurator) =>
                {
                    configurator.UseDelayedMessageScheduler();

                    configurator.Host(rabbitMqOptions.Host, "/", h =>
                    {
                        h.Username(rabbitMqOptions.Username);
                        h.Password(rabbitMqOptions.Password);
                    });
                                        
                    configurator.ConfigureEndpoints(context);

                    configurator.ReceiveEndpoint("discharge-processing", e =>
                    {
                        e.ConfigureConsumer<DischargeConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();
            services.AddScoped<IEventStore, EventStore>();

            services.AddAuthorization(options => PolicyFactory.BuildPolicies(options));

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");                
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}
