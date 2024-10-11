using System;
using MQBulkInsert.Application.Common.Interfaces.Repositories;
using MQBulkInsert.Domain.Entities;

namespace MQBulkInsert.Application.Common.Interfaces;

public interface IUnitOfWork
{

    IUserRepository UserRepository { get; }
    IFileProcessRepository FileProcessRepository { get; }
        // Task BulkInsertAsync(IEnumerable<User> entities);

    Task<int> SaveChangesAsync();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}