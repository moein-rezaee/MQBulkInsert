using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using MQBulkInsert.Api.Middlewares;
using MQBulkInsert.Application;
using MQBulkInsert.Application.Validators.User;
using MQBulkInsert.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.AddMediatR(typeof(Program).Assembly);


builder.Services.AddFluentValidationAutoValidation();
// builder.Services.AddValidatorsFromAssemblyContaining<ImportUserValidator>();

// builder.Services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;

builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 104857600; // 100MB
});

builder.Services.AddTransient<ErrorHandlingMiddleware>();


builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();


var app = builder.Build();
app.UseMiddleware<ErrorHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

