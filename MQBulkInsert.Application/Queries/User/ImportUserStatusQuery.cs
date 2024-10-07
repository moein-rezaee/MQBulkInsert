using System;
using MediatR;
using MQBulkInsert.Application.Models;

namespace MQBulkInsert.Application.Queries.User;

public record class ImportUserStatusQuery: IRequest<ProcessStatusModel>
{
    public required string TrackingId { get; set; }
}
