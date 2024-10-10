using System;
using Microsoft.AspNetCore.Http;
using MQBulkInsert.Application.Common.Interfaces;

namespace MQBulkInsert.Application.Models;

public record class FileDetails
{
    public static FileDetails GetInstance(IFormFile File, string Folder, string? NewName = null)
    {
        string fileName = Path.GetFileName(File.FileName);
        string newName = NewName ?? fileName;
        string ext = Path.GetExtension(File.FileName).Replace(".", "");
        string savePath = Path.Combine(Directory.GetCurrentDirectory(), Folder);
        string fullNewName = $"{NewName}.{ext}";
        return new()
        {
            Name = fileName,
            Extension = ext,
            NewName = newName,
            RelativePath = Folder,
            FullNewName = fullNewName,
            SavePath = savePath,
            FullPath = Path.Combine(savePath, fullNewName)
        };
    }

    public required string Name { get; set; }
    public required string Extension { get; set; }
    public required string FullNewName { get; set; }
    public required string NewName { get; set; }
    public required string RelativePath { get; set; }
    public required string SavePath { get; set; }
    public required string FullPath { get; set; }
}
