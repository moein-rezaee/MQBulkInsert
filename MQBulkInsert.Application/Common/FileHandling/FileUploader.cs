using System;
using Microsoft.AspNetCore.Http;
using MQBulkInsert.Application.Models;

namespace MQBulkInsert.Application.Common.FileHandling;

public class FileUploader(IFormFile file)
{
    public FileDetails details { get; private set; }

    private readonly IFormFile _file = file;

    private FileDetails getFileDetails(string NewName, string Folder = "Uploads")
    {
        return FileDetails.GetInstance(_file, Folder, NewName);
    }

    private FileLocator getFileLocator(FileDetails details)
    {
        return new FileLocator(_file, details);
    }

    public static async Task<FileDetails> UploadToAsync(IFormFile file, string newFileName, CancellationToken token)
    {
        FileUploader uploader = new(file);
        FileDetails details = uploader.getFileDetails(newFileName);
        FileLocator fileLocator = uploader.getFileLocator(details);
        await fileLocator.SaveAsync(token);
        return details;
    }

}
