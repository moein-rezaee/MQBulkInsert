using System;
using EFCore.BulkExtensions;
using MassTransit;
using Microsoft.Extensions.Logging;
using MQBulkInsert.Application.Exceptions;
using MQBulkInsert.Domain.Entities;
using MQBulkInsert.Domain.Enums;
using MQBulkInsert.Domain.Events.FileProcessing;
using MQBulkInsert.Infrastructure.Commands.FileHandling;
using MQBulkInsert.Infrastructure.Persistence;
using OfficeOpenXml;

namespace MQBulkInsert.Infrastructure.Messaging.Consumers.FileProcessing;

public class FileProcessingImportConsumer(
    ApplicationDbContext dbContext,
    ILogger<FileProcessingImportConsumer> logger) : IConsumer<FileProcessingImportEvent>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly ILogger<FileProcessingImportConsumer> _logger = logger;

    public async Task Consume(ConsumeContext<FileProcessingImportEvent> context)
    {
        string filePath = context.Message.FilePath;
        Guid id = context.Message.Id;
        if (File.Exists(filePath))
        {
            try
            {
                await UpdateFileStatus(id, ProcessStatus.Processing);
                IEnumerable<User> users = GetDataFromFileAsUsers(context.Message.Id, filePath);
                await BulkInsert(users);
                await UpdateFileStatus(id, ProcessStatus.Completed);
            }
            catch (Exception ex)
            {
                await UpdateFileStatus(id, ProcessStatus.Failed);
                _logger.LogError(ex, ex.Message);
            }

            await Task.CompletedTask;
        }
        else
        {
            FileNotFoundException ex = new("The specified file was not found.", filePath);
            _logger.LogError(ex, ex.Message);
            throw ex;
        }
    }

    private IEnumerable<User> GetDataFromFileAsUsers(Guid trackingId, string filePath)
    {
        using ExcelPackage package = FileExcelReader.GetInstance(filePath);
        var worksheet = package.Workbook.Worksheets[0];
        var rowCount = worksheet.Dimension.Rows;

        for (int row = 2; row <= rowCount; row++)
            yield return GetDataFromFileAsUser(trackingId, worksheet.Cells, row);
    }

    private User GetDataFromFileAsUser(Guid trackingId, ExcelRange cells, int row)
    {
        string FullName = cells[row, 1].Text;
        string Email = cells[row, 2].Text;
        string Mobile = cells[row, 3].Text;
        return new User()
        {
            Email = Email,
            FullName = FullName,
            Mobile = Mobile,
            FileTrackingId = trackingId
        };
    }

    private async Task BulkInsert(IEnumerable<User> users) => await _dbContext.BulkInsertAsync(users);

    private async Task UpdateFileStatus(Guid id, ProcessStatus status)
    {
        var fileRecord = await _dbContext.Files.FindAsync(id) ?? throw new NotFoundException($"Entity not found. TrackingId: {id}");
        fileRecord.Status = status;
        fileRecord.CompletedAt = DateTime.Now;
        await _dbContext.SaveChangesAsync();
    }

}
