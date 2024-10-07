using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace MQBulkInsert.Application.Commands.User;

public record class ImportUserCommand : IRequest<Guid> 
{
    public required IFormFile File { get; set; }
    public Guid TrackingId { get; set; } = Guid.NewGuid();
}
