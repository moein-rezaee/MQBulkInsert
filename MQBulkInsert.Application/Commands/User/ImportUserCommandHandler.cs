using MassTransit;
using MediatR;
using MQBulkInsert.Application.Common.FileHandling;
using MQBulkInsert.Application.Common.Interfaces;
using MQBulkInsert.Application.Models;
using MQBulkInsert.Domain.Entities;
using MQBulkInsert.Domain.Events.FileProcessing;

namespace MQBulkInsert.Application.Commands.User;

public class ImportUserCommandHandler : IRequestHandler<ImportUserCommand, Guid>
{

    private readonly IUnitOfWork _context;
    private readonly IBus _bus;

    public ImportUserCommandHandler(IUnitOfWork context, IBus bus)
    {
        _context = context;
        _bus = bus;
    }

    public async Task<Guid> Handle(ImportUserCommand request, CancellationToken cancellationToken)
    {
        // TODO: Set Upload Files Address in Settings
        string newFileName = request.TrackingId.ToString();
        FileDetails details = await FileUploader.UploadToAsync(request.File, newFileName, cancellationToken);

        FileProcessing record = new()
        {
            Id = request.TrackingId,
            FileName = details.FullNewName
        };
        await _context.FileProcessRepository.AddAsync(record);
        await _context.SaveChangesAsync(cancellationToken);

        FileProcessingImportEvent fileImportEvent = new()
        {
            Id = record.Id,
            FilePath = details.FullPath,
            Status = record.Status
        };
        await _bus.Publish(fileImportEvent, cancellationToken);
        return request.TrackingId;
    }
}
