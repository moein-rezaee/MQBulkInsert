using System;

namespace MQBulkInsert.Domain.Enums;

public enum ProcessStatus
{
    Pending,
    Processing,
    Completed,
    Failed
}
