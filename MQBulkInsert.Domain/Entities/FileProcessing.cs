using System;
using MQBulkInsert.Domain.Enums;

namespace MQBulkInsert.Domain.Entities;

public class FileProcessing
{
    public Guid Id { get; set; }
    public required string FileName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
    public ProcessStatus Status { get; set; } = ProcessStatus.Processing;

    public ICollection<User>? Users { get; set; }
}
