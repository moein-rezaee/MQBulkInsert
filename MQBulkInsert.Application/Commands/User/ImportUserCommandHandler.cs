using System;
using System.Xml.Linq;
using MassTransit;
using MediatR;
using MQBulkInsert.Application.Common.Interfaces;
using MQBulkInsert.Application.Events.FileProcessing;
using MQBulkInsert.Domain.Entities;

namespace MQBulkInsert.Application.Commands.User;

public class ImportUserCommandHandler : IRequestHandler<ImportUserCommand, Guid>
{

    private readonly IApplicationDbContext _context;
    private readonly IBus _bus;

    public ImportUserCommandHandler(IApplicationDbContext context, IBus bus)
    {
        _context = context;
        _bus = bus;
    }

    public async Task<Guid> Handle(ImportUserCommand request, CancellationToken cancellationToken)
    {
        // TODO: Set Upload Files Address in Settings
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        string fileName = request.File.FileName;
        string fileExtension = Path.GetExtension(fileName);
        string newName = request.TrackingId + fileExtension;
        string filePath = Path.Combine(folderPath, newName);
        using var stream = new FileStream(filePath, FileMode.Create);
        await request.File.CopyToAsync(stream, cancellationToken);

        FileProcessing record = new() {
            Id = request.TrackingId,
            FileName = newName
        }; 
        _context.Files.Add(record);
        await _context.SaveChangesAsync(cancellationToken);

        FileProcessingImportEvent fileImportEvent = new () {
            Id = record.Id,
            FileName = record.FileName,
            Status = record.Status
        };
        await _bus.Publish(fileImportEvent, cancellationToken);

        return request.TrackingId;
    }
}
