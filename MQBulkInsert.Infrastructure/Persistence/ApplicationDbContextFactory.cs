using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MQBulkInsert.Infrastructure.Persistence;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<ApplicationDbContext> optionBuilder = new();

        // var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
        // if (string.IsNullOrEmpty(connectionString))
        // {
        //     throw new InvalidOperationException("Database connection string is not set in environment variables.");
        // }

        // optionBuilder.UseSqlServer(connectionString);

        const string CONNECTION_STRING = "Server=localhost; Persist Security Info=False; TrustServerCertificate=true; User ID=sa;Password=admin@123;Initial Catalog=MQBulkInsert;";
        optionBuilder.UseSqlServer(CONNECTION_STRING);
        return new ApplicationDbContext(optionBuilder.Options);
    }
}