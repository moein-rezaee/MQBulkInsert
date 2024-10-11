using EFCore.BulkExtensions;
using MQBulkInsert.Application.Common.Interfaces.Repositories;
using MQBulkInsert.Domain.Entities;

namespace MQBulkInsert.Infrastructure.Persistence.Repositories;

public class UserRepository: IUserRepository
{

    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task BulkInsert(IEnumerable<User> users)
    {
        await _context.BulkInsertAsync(users);
    }

}
