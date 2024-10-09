using System;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MQBulkInsert.Application.Common.Interfaces;
using MQBulkInsert.Infrastructure.Persistence;

namespace MQBulkInsert.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddMassTransit(option =>
        {
            option.UsingRabbitMq((context, config) =>
            {
                config.Host("localhost", "/", host =>
                {
                    host.Username("admin");
                    host.Password("admin@123");
                });
            });
        });
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));


        return services;
    }
}