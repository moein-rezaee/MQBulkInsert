using System;
using MediatR;
using MQBulkInsert.Application.Models;
using MQBulkInsert.Domain.Enums;

namespace MQBulkInsert.Application.Queries.User;

public class ImportUserStatusQueryHandler: IRequestHandler<ImportUserStatusQuery, ProcessStatusModel>
{
    public async Task<ProcessStatusModel> Handle(ImportUserStatusQuery request, CancellationToken cancellationToken)
    {
        // TODO: Implement logic to get the process status based on TrackingId
        return new ProcessStatusModel
        {
            TrackingId = request.TrackingId,
            Status = ProcessStatus.Pending
        };
    }
}
