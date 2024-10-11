using System;
using OfficeOpenXml;

namespace MQBulkInsert.Infrastructure.Commands.FileHandling;

public class FileExcelReader
{
    public static ExcelPackage GetInstance(string filePath)
    {
        ExcelPackage.LicenseContext = LicenseContext.Commercial;
        FileInfo info = new(filePath);
        return new ExcelPackage(info);
    }
}
