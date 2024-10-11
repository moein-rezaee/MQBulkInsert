using EFCore.BulkExtensions;
using MQBulkInsert.Application.Common.Interfaces;
using MQBulkInsert.Application.Common.Interfaces.Repositories;
using MQBulkInsert.Domain.Entities;

namespace MQBulkInsert.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{

    private readonly ApplicationDbContext _context;
    public IUserRepository UserRepository { get; }
    public IFileProcessRepository FileProcessRepository { get; }

    public UnitOfWork(ApplicationDbContext context, IUserRepository userRepository, IFileProcessRepository fileProcessRepository)
    {
        _context = context;
        UserRepository = userRepository;
        FileProcessRepository = fileProcessRepository;
    }



    public async Task BulkInsertAsync(IEnumerable<User> users)
    {
        await _context.BulkInsertAsync(users);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
