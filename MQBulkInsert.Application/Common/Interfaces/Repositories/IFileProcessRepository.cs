using System;
using MQBulkInsert.Domain.Entities;
using MQBulkInsert.Domain.Enums;

namespace MQBulkInsert.Application.Common.Interfaces.Repositories;

public interface IFileProcessRepository
{
    Task UpdateStatus(Guid id, ProcessStatus status);
    Task AddAsync(FileProcessing fileProcess);
}
