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
        const string FIELD_NAME = "فیلد فایل";

        RuleFor(x => x.File)
            .NotNull()
            .WithName(FIELD_NAME)
            .WithMessage("فیلد فایل نمی تواند خالی باشد");

         RuleFor(x => x.File.Length)
            .NotEqual(0)
            .WithName(FIELD_NAME)
            .WithMessage("فیلد فایل نمی تواند خالی باشد");

        RuleFor(x => x.File.Length)
            .LessThanOrEqualTo(20 * 1024 * 1024)
            .WithName(FIELD_NAME)
            .WithMessage("حجم فایل باید کمتر از 20 مگابایت باشد");

        RuleFor(x => x.File.ContentType)
            .Must(IsValidFileType)
            .WithName(FIELD_NAME)
            .WithMessage("نوع فایل انتخاب شده معتبر نمی باشد. پسوند های مجاز: .xls, .xlsx, .xlsm");
    }

    private bool IsValidFileType(string fileType) => ValidMimeTypes.Contains(fileType);
}
