using MediatR;
using MQBulkInsert.Application;
using MQBulkInsert.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(ApplicationAssemblyMarker).Assembly);
builder.Services.AddControllers();
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // اطمینان از وجود رشته اتصال

string? connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
Environment.SetEnvironmentVariable("DB_CONNECTION_STRING", connectionString);

if (!string.IsNullOrEmpty(connectionString))
    builder.Services
        .AddInfrastructure(connectionString)
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

