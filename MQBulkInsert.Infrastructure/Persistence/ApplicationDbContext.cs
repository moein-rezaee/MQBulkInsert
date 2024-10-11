using Microsoft.EntityFrameworkCore;
using MQBulkInsert.Application.Common.Interfaces;
using MQBulkInsert.Domain.Entities;

namespace MQBulkInsert.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        // Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FileProcessing>()
            .HasMany(e => e.Users)
            .WithOne(u => u.File)
            .HasForeignKey(u => u.FileTrackingId);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<FileProcessing> Files { get; set; }
}
