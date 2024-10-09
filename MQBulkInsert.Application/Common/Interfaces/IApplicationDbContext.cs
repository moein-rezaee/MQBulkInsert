using System;
using Microsoft.EntityFrameworkCore;
using MQBulkInsert.Domain.Entities;

namespace MQBulkInsert.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<FileProcessing> Files { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}