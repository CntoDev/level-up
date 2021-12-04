using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Roster.Web.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Roster.Core.Services;
using Roster.Core.Storage;
using Roster.Infrastructure.Storage;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using MassTransit;
using Roster.Infrastructure.Consumers;
using Roster.Core.Events;
using Roster.Infrastructure.Events;
using Roster.Infrastructure.Configurations;
using Roster.Infrastructure;
using Microsoft.Extensions.Logging;
using Serilog;
using Roster.Web.Security;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Http;

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
            Log.Information("Configuring services...");
            Log.Information("ConnectionStrings {roster} {postgres}", Configuration.GetConnectionString("Roster"), Configuration.GetConnectionString("PostgresConnection"));
            Log.Information("MailJet {@mailjetOptions}", Configuration.GetSection("MailJet").Get<MailJetOptions>());
            Log.Information("RabbitMq {@rabbitmqOptions}", Configuration.GetSection("RabbitMq").Get<RabbitMqOptions>());
            #endregion

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("PostgresConnection")));
            services.AddDbContext<RosterDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("Roster"), o => o.MigrationsAssembly("Roster.Web")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();

            // Roster Core registrations here
            services.AddScoped<IApplicationStorage, DatabaseApplicationStorage>();
            services.AddScoped<IMemberStorage, DatabaseMemberStorage>();
            services.AddScoped<IRankStorage, DatabaseRankStorage>();
            services.AddScoped<IDlcStorage, DatabaseDlcStorage>();
            services.AddScoped<IEventStateStorage, DatabaseEventStateStorage>();
            
            services.AddScoped<IDiscordValidationService, DummyDiscordValidationService>();
            services.AddScoped<ApplicationFormService>();
            services.AddScoped<MemberService>();

            // MailJet registrations
            services.AddSingleton<MailJetOptions>(sp =>
            {
                return Configuration.GetSection("MailJet").Get<MailJetOptions>();
            });

            services.AddSingleton<EmailService>();

            // Add Mass Transit
            RabbitMqOptions rabbitMqOptions = Configuration.GetSection("RabbitMq").Get<RabbitMqOptions>();

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(rabbitMqOptions.Host, "/", h => {
                        h.Username(rabbitMqOptions.Username);
                        h.Password(rabbitMqOptions.Password);
                    });
                    configurator.ConfigureEndpoints(context);
                });

                x.AddConsumer<MemberCreationConsumer>();
                x.AddConsumer<EmailSender>();
                x.AddConsumer<EventSink>();
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
                app.UseHsts();
            }

            // Location enforcing to https if https request because of http issues on production server (something is blocking http traffic)
            app.Use(async (context, next) => {
                context.Response.OnStarting(() => {
                    int statusCode = context.Response.StatusCode;

                    if (statusCode == 301 || statusCode == 302) {
                        string locationUrl = context.Response.Headers["Location"];
                        Log.Information($"Redirecting scheme {context.Request.Scheme} to {locationUrl}");

                        if (locationUrl.Contains("http:")) {
                            locationUrl = locationUrl.Replace("http:","https:");
                            Log.Warning($"Fixing scheme to {locationUrl}");
                            context.Response.Headers["Location"] = locationUrl;
                        }
                    }

                    return Task.CompletedTask;
                });

                await next.Invoke();
            });

            app.UseHttpsRedirection();
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
