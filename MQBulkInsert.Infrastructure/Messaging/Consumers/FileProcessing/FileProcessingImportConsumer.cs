using System;
using MassTransit;
using MQBulkInsert.Application.Events.FileProcessing;
using MQBulkInsert.Infrastructure.Persistence;
using OfficeOpenXml;

namespace MQBulkInsert.Infrastructure.Messaging.Consumers.FileProcessing;

public class FileProcessingImportConsumer : IConsumer<FileProcessingImportEvent>
{
    private readonly ApplicationDbContext _dbContext;
    public FileProcessingImportConsumer(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task Consume(ConsumeContext<FileProcessingImportEvent> context)
    {
        var filePath = context.Message.FilePath;

        if (File.Exists(filePath))
        {
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                FileInfo info = new(filePath);
                using ExcelPackage package = new(info);

                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    string FullName = worksheet.Cells[row, 1].Text;
                    string Email = worksheet.Cells[row, 2].Text;
                    string Mobile = worksheet.Cells[row, 3].Text;

                }
            }
            catch (System.Exception ex)
            {
                // TODO: Log Error
                var err = ex;
            }

            await Task.CompletedTask;
        }
        else
        {
            // TODO: Log Error
            throw new FileNotFoundException("The specified file was not found.", filePath);
        }
    }

}