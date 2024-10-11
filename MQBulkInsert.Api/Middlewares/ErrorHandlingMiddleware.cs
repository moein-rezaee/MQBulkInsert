using System.Net;
using FluentValidation;
using Newtonsoft.Json;

namespace MQBulkInsert.Api.Middlewares;
// public class ErrorHandlingMiddleware : IMiddleware
// {
//     private readonly ILogger<ErrorHandlingMiddleware> _logger;

//     public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) => _logger = logger;

//     public async Task InvokeAsync(HttpContext context, RequestDelegate next)
//     {
//         try
//         {
//             await next(context);
//         }
//         catch (ValidationException exp)
//         {
//             _logger.LogInformation(exp, exp.Message);

//             context.Response.StatusCode = 400;
//             context.Response.ContentType = "application/json; charset=utf-8";
//             await context.Response.WriteAsync(exp.Message);
//         }
//         catch (Exception exp)
//         {
//             _logger.LogError(exp, exp.Message);

//             context.Response.StatusCode = 500;
//             context.Response.ContentType = "application/json; charset=utf-8";
//             await context.Response.WriteAsync(exp.Message);
//         }
//     }
// }


// public class ErrorHandlingMiddleware
// {
//     private readonly RequestDelegate _next;

//     public ErrorHandlingMiddleware(RequestDelegate next)
//     {
//         _next = next;
//     }

//     public async Task Invoke(HttpContext context)
//     {
//         try
//         {
//             await _next(context);
//         }
//         catch (Exception ex)
//         {
//             await HandleExceptionAsync(context, ex);
//         }
//     }

//     private Task HandleExceptionAsync(HttpContext context, Exception ex)
//     {
//         var code = HttpStatusCode.InternalServerError; // 500 if unexpected

//         // Set custom error responses for specific exceptions
//         if (ex is NotFoundException) code = HttpStatusCode.NotFound;
//         else if (ex is ValidationException) code = HttpStatusCode.BadRequest;

//         var result = JsonConvert.SerializeObject(new { error = ex.Message });
//         context.Response.ContentType = "application/json";
//         context.Response.StatusCode = (int)code;
//         return context.Response.WriteAsync(result);
//     }
// }


public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            await HandleValidationExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleValidationExceptionAsync(HttpContext context, ValidationException ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        var errors = ex.Errors.Select(e => new
        {
            Field = e.FormattedMessagePlaceholderValues.FirstOrDefault(i => i.Key == "PropertyName").Value ?? e.PropertyName,
            e.ErrorMessage
        }).ToList();
        var result = JsonConvert.SerializeObject(new { errors });
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(result);
    }
    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        _logger.LogError(ex, ex.Message);
        var result = JsonConvert.SerializeObject(new { message = "خطای سرور. لطفا با پشتیبانی تماس بگیرید." });
        context.Response.ContentType = "application/json";
        return context.Response.WriteAsync(result);
    }
}
