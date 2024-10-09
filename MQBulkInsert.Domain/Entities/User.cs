namespace MQBulkInsert.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Mobile { get; set; }

    public Guid FileTrackingId { get; set; }
    public FileProcessing? File { get; set; }
}
