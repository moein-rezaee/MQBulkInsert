using System;
using FluentValidation;
using MQBulkInsert.Application.Commands.User;

namespace MQBulkInsert.Application.Validators.User;

public class ImportUserValidator : AbstractValidator<ImportUserCommand>
{
    private static readonly string[] ValidMimeTypes = [
        "application/vnd.ms-excel", //.xls
        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", //.xlsx
        "application/vnd.ms-excel.sheet.macroEnabled.12" //.xlsm
    ];
    public ImportUserValidator()
    {
        // RuleFor(x => x.File)
        //     .NotNull()
        //     .WithMessage("فیلد فایل نمی تواند خالی باشد");

        //  RuleFor(x => x.File.Length)
        //     .NotEqual(0)
        //     .WithMessage("فیلد فایل نمی تواند خالی باشد");

        RuleFor(x => x.File.Length)
            .LessThanOrEqualTo(20 * 1024 * 1024)
            .WithMessage("حجم فایل باید کمتر از 20 مگابایت باشد");

        RuleFor(x => x.File.ContentType)
            .Must(IsValidFileType)
            .WithMessage("نوع فایل انتخاب شده معتبر نمی باشد. پسوند های مجاز: .xls, .xlsx, .xlsm");
    }

    private bool IsValidFileType(string fileType) => ValidMimeTypes.Contains(fileType);
}
