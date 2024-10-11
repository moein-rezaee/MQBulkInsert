using Microsoft.Extensions.Logging;
using MQBulkInsert.Application.Common.Interfaces;
using MQBulkInsert.Domain.Entities;
using MQBulkInsert.Domain.Enums;
using MQBulkInsert.Infrastructure.Commands.FileHandling;
using OfficeOpenXml;

namespace MQBulkInsert.Application.EventHandlers.FileProcessing;

public class FileProcessingEventHandler(IUnitOfWork dbContext, ILogger<FileProcessingEventHandler> logger)
{
    private readonly IUnitOfWork _dbContext = dbContext;
    private readonly ILogger<FileProcessingEventHandler> _logger = logger;

    public async Task Consume(Guid id, string filePath)
    {
        if (File.Exists(filePath))
        {
            try
            {
                await UpdateFileStatus(id, ProcessStatus.Processing);
                IEnumerable<User> users = GetDataFromFileAsUsers(id, filePath);
                await BulkInsert(users);
                await UpdateFileStatus(id, ProcessStatus.Completed);
            }
            catch (Exception ex)
            {
                await UpdateFileStatus(id, ProcessStatus.Failed);
                _logger.LogError(ex, ex.Message);
            }
        }
        else
        {
            FileNotFoundException ex = new($"فایل موردنظر یافت نشد: {filePath}");
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

    private async Task BulkInsert(IEnumerable<User> users) =>  await _dbContext.UserRepository.BulkInsert(users);
    private async Task UpdateFileStatus(Guid id, ProcessStatus status) => await _dbContext.FileProcessRepository.UpdateStatus(id, status);

}