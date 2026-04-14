using Microsoft.EntityFrameworkCore;
using Repair_Shop_App_Api.Data;
using Repair_Shop_App_Api.Models;
using Repair_Shop_App_Api.Repositories;
using Repair_Shop_App_Api.Services;

var builder = WebApplication.CreateBuilder(args);

// SERVICES
builder.Services.AddScoped<DeviceTypesRepository>();
builder.Services.AddScoped<DeviceTypesService>();

builder.Services.AddScoped<DevicesRepository>();
builder.Services.AddScoped<DevicesService>();

builder.Services.AddScoped<RepairsRepository>();
builder.Services.AddScoped<RepairsService>();

builder.Services.AddScoped<StatusStepsRepository>();
builder.Services.AddScoped<StatusStepsService>();

builder.Services.AddScoped<RepairStatusHistoryRepository>();
builder.Services.AddScoped<RepairStatusHistoryService>();

// DB (POSTGRESQL)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddControllers()
.AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// MIDDLEWARE
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAngular");
app.UseAuthorization();
app.MapControllers();

// TEST ROUTE
app.MapGet("/", () => "Repair Shop API is running 🚀");

app.Run();