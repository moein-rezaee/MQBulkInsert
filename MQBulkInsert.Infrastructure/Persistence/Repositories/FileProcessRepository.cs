using MQBulkInsert.Application.Common.Interfaces.Repositories;
using MQBulkInsert.Application.Exceptions;
using MQBulkInsert.Domain.Entities;
using MQBulkInsert.Domain.Enums;

namespace MQBulkInsert.Infrastructure.Persistence.Repositories;

public class FileProcessRepository : IFileProcessRepository
{
    private readonly ApplicationDbContext _context;

    public FileProcessRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(FileProcessing fileProcess)
    {
        await _context.Files.AddAsync(fileProcess);
    }


    public async Task<FileProcessing> FindAsync(Guid id)
    {
        return await _context.Files.FindAsync(id) ?? throw new NotFoundException($"رکورد فایل موردنظر یافت نشد: {id}");
    }


    public async Task UpdateStatus(Guid id, ProcessStatus status)
    {
        FileProcessing fileRecord = await FindAsync(id);
        fileRecord.Status = status;
        fileRecord.CompletedAt = DateTime.Now;
        await _context.SaveChangesAsync();
    }
}