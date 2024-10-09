using System;
using MediatR;
using MQBulkInsert.Application.Common.Interfaces;

namespace MQBulkInsert.Application.Commands.User;

public class ImportUserCommandHandler : IRequestHandler<ImportUserCommand, Guid>
{

    private readonly IApplicationDbContext _context;

    public ImportUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
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
        string filePath = Path.Combine(folderPath, request.TrackingId + fileExtension);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await request.File.CopyToAsync(stream, cancellationToken);
        }

        var files = _context.Files.ToList();

        return request.TrackingId;
    }
}
