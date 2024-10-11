using System;
using MQBulkInsert.Domain.Entities;

namespace MQBulkInsert.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    Task BulkInsert(IEnumerable<User> users);
    
}
