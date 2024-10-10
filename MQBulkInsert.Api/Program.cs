using MediatR;
using MQBulkInsert.Application;
using MQBulkInsert.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddMediatR(typeof(ApplicationAssemblyMarker).Assembly);

string? connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
Environment.SetEnvironmentVariable("DB_CONNECTION_STRING", connectionString);

if (!string.IsNullOrEmpty(connectionString))
    builder.Services
        .AddInfrastructure(builder.Configuration)
        .AddApplication();
else
    throw new Exception("connection string not found!");

var app = builder.Build();

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

