using System;
using Microsoft.AspNetCore.Http;
using MQBulkInsert.Application.Models;

namespace MQBulkInsert.Application.FileHandling;

public class FileLocator(IFormFile file, FileDetails details)
{
    private readonly IFormFile _file = file;
    private readonly FileDetails _details = details;

    private void checkSavePath()
    {
        if (!Directory.Exists(_details.SavePath))
        {
            Directory.CreateDirectory(_details.SavePath);
            //TODO: Throw Exception IF Don't Create Folder
        }
    }

    public async 
    Task
SaveAsync(CancellationToken token)
    {
        checkSavePath();
        using var stream = new FileStream(details.FullPath, FileMode.Create);
        await file.CopyToAsync(stream, token);
    }
}
