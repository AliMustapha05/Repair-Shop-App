using Microsoft.EntityFrameworkCore;
using Repair_Shop_App_Api.Data;
using Repair_Shop_App_Api.Models;
using Repair_Shop_App_Api.Repositories;
using Repair_Shop_App_Api.Services;

var builder = WebApplication.CreateBuilder(args);

// =============================
// SERVICES
// =============================

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

// =============================
// DB CONTEXT
// =============================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// =============================
// CORS (PRODUCTION SAFE)
// =============================
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

// Controllers + JSON
builder.Services.AddControllers()
.AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();

// Swagger ALWAYS ENABLED (FIX FOR RENDER 404)
builder.Services.AddSwaggerGen();

var app = builder.Build();

// =============================
// MIDDLEWARE
// =============================

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAngular");

app.UseAuthorization();

app.MapControllers();

// =============================
// SAFE SEEDING (NO MIGRATE ON RENDER)
// =============================
using (var scope = app.Services.CreateScope())
{
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // ❌ DO NOT RUN MIGRATE ON RENDER
        // db.Database.Migrate();

        if (!db.DeviceTypes.Any())
        {
            db.DeviceTypes.AddRange(
                new DeviceTypes { Name = "Mobile", IsActive = true },
                new DeviceTypes { Name = "Laptop", IsActive = true },
                new DeviceTypes { Name = "Tablet", IsActive = true },
                new DeviceTypes { Name = "Desktop", IsActive = true }
            );

            db.SaveChanges();
        }

        if (!db.StatusSteps.Any())
        {
            db.StatusSteps.AddRange(
                new StatusSteps { Name = "Received", SortOrder = 1, IsActive = true },
                new StatusSteps { Name = "Diagnosed", SortOrder = 2, IsActive = true },
                new StatusSteps { Name = "Waiting for parts", SortOrder = 3, IsActive = true },
                new StatusSteps { Name = "In progress", SortOrder = 4, IsActive = true },
                new StatusSteps { Name = "Quality check", SortOrder = 5, IsActive = true },
                new StatusSteps { Name = "Ready for pickup", SortOrder = 6, IsActive = true },
                new StatusSteps { Name = "Returned", SortOrder = 7, IsActive = true },
                new StatusSteps { Name = "Canceled", SortOrder = 8, IsActive = true }
            );

            db.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("DB SEED ERROR (ignored): " + ex.Message);
    }
}

app.MapGet("/", () => "Repair Shop API is running 🚀");

app.Run();