using Application.Interfaces;
using Application.Mappings;
using Application.Services;
using Domain.Interfaces;
using Infra.Data.Context;
using Infra.Data.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Infra.Data.Queue;
using Infra.Data.Configurations;
using Application.UseCase; 
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;

namespace Infra.InversionOfControl
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(
            options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), o => o.MigrationsAssembly(typeof(StoreDbContext).Assembly.FullName));
            }
            );

            services.Configure<RabbitMqConfiguration>(a => configuration.GetSection(nameof(RabbitMqConfiguration)).Bind(a));
            services.AddSingleton<IQueueService, RabbitMqService>();

            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPaymentUseCase, PaymentUseCase>();
            services.AddAutoMapper(typeof(PaymentDomainMappingProfile).Assembly);

            var rabbitMqConfiguration = configuration.GetSection(nameof(RabbitMqConfiguration)).Get<RabbitMqConfiguration>();

            var connectionString = $"amqp://{rabbitMqConfiguration.Username}:{rabbitMqConfiguration.Password}@{rabbitMqConfiguration.HostName}";
            services
                .AddHealthChecks()
                .AddRabbitMQ(connectionString, name: "rabbitmq-check", tags: new string[] { "rabbitmq" })
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "sqlserver-check", tags: new string[] { "sqlserver" });

            return services;
        } 

        public static IApplicationBuilder ConfigureHealthCheck(this IApplicationBuilder builder)
        {
            builder.UseHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
            {
                Predicate = p => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            return builder;
        }
    }
}
