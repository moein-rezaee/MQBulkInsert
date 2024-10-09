using System;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MQBulkInsert.Application.Common.Interfaces;
using MQBulkInsert.Application.Models;
using MQBulkInsert.Domain.Entities;
using MQBulkInsert.Domain.Enums;

namespace MQBulkInsert.Application.Queries.User;

public class ImportUserStatusQueryHandler : IRequestHandler<ImportUserStatusQuery, ProcessStatusModel>
{
    private readonly IApplicationDbContext _context;

    public ImportUserStatusQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ProcessStatusModel> Handle(ImportUserStatusQuery request, CancellationToken cancellationToken)
    {
        FileProcessing file = await _context.Files
            .Where(i => i.Id == new Guid(request.TrackingId))
            .FirstOrDefaultAsync(cancellationToken);
        return new ProcessStatusModel
        {
            TrackingId = request.TrackingId,
            Status = file.Status.ToString()
        };
    }
}
