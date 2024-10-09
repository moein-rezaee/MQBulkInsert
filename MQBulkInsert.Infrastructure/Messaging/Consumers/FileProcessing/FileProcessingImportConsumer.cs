using System;
using MassTransit;
using MQBulkInsert.Application.Events.FileProcessing;

namespace MQBulkInsert.Infrastructure.Messaging.Consumers.FileProcessing;

public class FileProcessingImportConsumer: IConsumer<FileProcessingImportEvent>
{
    public async Task Consume(ConsumeContext<FileProcessingImportEvent> context)
    {
        var fileInfo = context.Message;

        await Task.CompletedTask;
    }
}