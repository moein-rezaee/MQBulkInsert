using MassTransit;
using Microsoft.Extensions.Logging;
using MQBulkInsert.Application.Common.Interfaces;
using MQBulkInsert.Application.EventHandlers.FileProcessing;
using MQBulkInsert.Domain.Events.FileProcessing;

namespace MQBulkInsert.Infrastructure.Messaging.Consumers.FileProcessing;

public class FileProcessingImportConsumer : IConsumer<FileProcessingImportEvent>
{
    private readonly FileProcessingEventHandler _eventHandler;

    public FileProcessingImportConsumer(IUnitOfWork dbContext, ILogger<FileProcessingEventHandler> logger)
    {
        _eventHandler = new(dbContext, logger);
    }

    public async Task Consume(ConsumeContext<FileProcessingImportEvent> context)
    {
        await _eventHandler.Consume(context.Message.Id, context.Message.FilePath);
        await Task.CompletedTask;
    }

}