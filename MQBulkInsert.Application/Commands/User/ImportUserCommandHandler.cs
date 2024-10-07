using System;
using MediatR;

namespace MQBulkInsert.Application.Commands.User;

public class ImportUserCommandHandler: IRequestHandler<ImportUserCommand, Guid>
{
    public async Task<Guid> Handle(ImportUserCommand request, CancellationToken cancellationToken)
    {
        return request.TrackingId; 
    }
}
