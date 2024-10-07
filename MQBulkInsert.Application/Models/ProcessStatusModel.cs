using System;
using MQBulkInsert.Domain.Enums;

namespace MQBulkInsert.Application.Models;

public record class ProcessStatusModel
{
    public required string TrackingId { get; set; }
    public ProcessStatus Status { get; set; }
}
