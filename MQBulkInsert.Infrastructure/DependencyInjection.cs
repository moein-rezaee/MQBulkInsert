using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MQBulkInsert.Application.Common.Interfaces;
using MQBulkInsert.Infrastructure.Messaging.Consumers.FileProcessing;
using MQBulkInsert.Infrastructure.Persistence;
using MQBulkInsert.Application.Exceptions;
using MQBulkInsert.Application.Common.Interfaces.Repositories;
using MQBulkInsert.Infrastructure.Persistence.Repositories;

namespace MQBulkInsert.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationManager configuration)
    {
        string connectionString = GetConfig(configuration, "ConnectionStrings:DefaultConnection");
        string hostUrl = GetConfig(configuration, "RabbitMQ:Host");
        string user = GetConfig(configuration, "RabbitMQ:Username");
        string pass = GetConfig(configuration, "RabbitMQ:Password");

        services.AddMassTransit(option =>
        {
            option.UsingRabbitMq((context, config) =>
            {
                config.Host(hostUrl, host =>
                {
                    host.Username(user);
                    host.Password(pass);
                });

                config.ConfigureEndpoints(context);
            });
            option.AddConsumer<FileProcessingImportConsumer>();
        });
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFileProcessRepository, FileProcessRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }

    private static string GetConfig(IConfigurationManager configuration, string key)
        => configuration[key] ?? throw new NotFoundException($"${key} Not Found");
}