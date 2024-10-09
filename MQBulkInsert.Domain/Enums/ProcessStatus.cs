using System;
using System.ComponentModel.DataAnnotations;

namespace MQBulkInsert.Domain.Enums;

public enum ProcessStatus
{
    [Display(Name = "در صف انتظار")]
    Pending,

    [Display(Name = "در حال پردازش")]
    Processing,

    [Display(Name = "ذخیره شده در پایگاه داده")]
    Completed,
    
    [Display(Name = "عملیات ذخیره سازی داده با مشکل مواجه شده")]
    Failed
}
