using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MQBulkInsert.Application.Exceptions;

namespace MQBulkInsert.Infrastructure.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        string appSettingsPath = Path.Combine(AppContext.BaseDirectory, @"../../../../MQBulkInsert.Api/appsettings.json");

        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(appSettingsPath, optional: false, reloadOnChange: true)
            .Build();

        string connectionString = GetConfig(config, "DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
        return new ApplicationDbContext(optionsBuilder.Options);
    }

    private static string GetConfig(IConfigurationRoot configuration, string key)
    => configuration.GetConnectionString(key) ?? throw new NotFoundException($"${key} Not Found");
}
