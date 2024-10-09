using System;
using MQBulkInsert.Domain.Enums;

namespace MQBulkInsert.Application.Events.FileProcessing;

public class FileProcessingImportEvent
{
    public Guid Id { get; set; }
    public required string FilePath { get; set; }
    public ProcessStatus Status { get; set; } = ProcessStatus.Processing;
}
